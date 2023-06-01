import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import Navigation from './components/Navigation/Navigation';
import NewEntryPage from './components/NewEntryPage/NewEntryPage';
import DayView from './components/DayView/DayView'
import apiService from './apiService';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

const localizer = momentLocalizer(moment);

const views = {
	month: true,
	// week: true,
	// day: true
};

const App = () => {

	const [events, setEvents] = useState([]);
	const [days, setDays] = useState([]);

	const handleSaveEntry = (entry) => {
		setEvents([...events, entry]);
	};

	useEffect(() => {
		var date = new Date();
		var start = new Date(date.getFullYear(), date.getMonth(), 1);
		var end = new Date(date.getFullYear(), date.getMonth() + 1, 0);
		fetchEvents(start, end);
	}, []);

	const fetchEvents = async (start, end) => {
		try {
			const data = await apiService.getEntries(start, end);

			var entries = data.entries.map(value => ({
				title: value.description + ' | ' + value.duration + ' | ' + String(value.isUtilization),
				start: value.onDate,
				end: new Date(value.onDate),
			}));
			setEvents(entries);

			var rawDays = await data.days;
			setDays(rawDays);
		} catch (error) {
			console.error('Error fetching events:', error);
		}
	};

	const handleNavigate = (date, view) => {
		const start = moment(date).startOf(view).toDate();
		const end = moment(date).endOf(view).toDate();
		fetchEvents(start, end);
	};

	const CustomDateHeader = ({ date, events }) => {
		const year = date.getFullYear();
		const month = date.getMonth() + 1;
		const dayOfMonth = date.getDate();

		var dayData = days.find(day => day.year === year && day.month === month && day.dayOfMonth === dayOfMonth);

		if (typeof dayData == 'undefined') {
			dayData = { year: year, month: month, dayOfMonth: dayOfMonth, total: 0.5, utilizedTotal: 0.0, numEntries: 0.0 };
		}

		return (
			<div className='custom-date-header'>
				<div>{dayData.dayOfMonth || dayOfMonth}</div>
				<div>
					<small>Num:{dayData.numEntries}</small>&nbsp;&nbsp;
					<small>Billable:{dayData.utilizedTotal.toFixed(2)}</small>&nbsp;&nbsp;
					<small>Total:{dayData.total.toFixed(2)}</small>
				</div>
			</div>
		);
	};

	const components = {
		month: {
			dateHeader: CustomDateHeader,
			event: (event) => (
				<div className='custom-event'><small>{event.title}</small></div>
			)
		}
	};

	return (
		<Router>
			<div className="container-fluid app">
				<Navigation />
				{/* <nav>
					<ul>
						<li><Link to="/">Calendar</Link></li>
						<li><Link to="/entry">New Entry For Today</Link></li>
						<li><Link to="/day-view">Day View</Link></li>
					</ul>
				</nav> */}
				<Routes>
					<Route path="/" element={
						<Calendar
							localizer={localizer}
							events={events}
							startAccessor="start"
							endAccessor="end"
							className="full-height-calendar"
							views={views}
							components={components}
							onNavigate={handleNavigate}
						/>} />
					{/* <Route path="/entry" element={<NewEntryPage onSave={handleSaveEntry} />} /> */}
					<Route path="day-view" element={<DayView />} />
				</Routes>
			</div>
		</Router>
	);
};

export default App;