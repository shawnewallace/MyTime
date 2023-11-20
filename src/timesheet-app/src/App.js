import { useAuth0 } from "@auth0/auth0-react";
import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import Navigation from './components/Navigation/Navigation';
import EntryForm from './components/EntryForm/EntryForm';
import DayView from './components/DayView/DayView'
import RangeView from './components/Reporting/RangeView'
import SummaryByWeek from './components/Reporting/SummaryByWeek'
import apiService from './apiService';
import Callback from './components/Callback/Callback';
import Profile from './components/Profile/Profile';
import Categories from './components/Categories/Categories';
import { PageLoader } from './components/page-loader';
import { AuthenticationGuard } from './components/authentication-guard';
import 'bootstrap/dist/css/bootstrap.min.css';
import "bootstrap-icons/font/bootstrap-icons.css";
import './App.css';

const localizer = momentLocalizer(moment);

const views = {
	month: true,
	// week: true,
	// day: true
};

const App = () => {

	const { isLoading } = useAuth0();


	const [events, setEvents] = useState([]);
	const [days, setDays] = useState([]);
	const [categories, setCategories] = useState([]);

	const handleSaveEntry = async (entry) => {
		await apiService.createEntry(entry);
	};

	useEffect(() => {
		var date = new Date();
		var start = new Date(date.getFullYear(), date.getMonth(), 1);
		var end = new Date(date.getFullYear(), date.getMonth() + 1, 0);
		fetchEvents(start, end);
		fetchCategories();
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

	const fetchCategories = async () => {
		try {
			const data = await apiService.getCategories();
			setCategories(data);
		} catch (error) {
			console.error('Error fetching categories:', error);
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
				<div>
					<Link to={`/day-view/${date.toISOString()}`} cats={categories}>
						{dayData.dayOfMonth || dayOfMonth}
					</Link>
				</div>
				<div>
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

	const blankEntry = {};

	if (isLoading) {
		return (
			<div className="page-layout">
				<PageLoader />
			</div>
		);
	}

	return (
		<Router>
			<div className="d-flex flex-column min-vh-100 app">
				<header className="bg-light">
					<div>
						<Navigation />
					</div>
				</header>
				<main className="flex-grow-1 ms-3 me-3 mb-3">
					<div className='card h-100'>
						<div className='card-body'>
							<Routes>
								<Route name="home" path="/" element={
									<Calendar
										localizer={localizer} 
										events={events} 
										startAccessor="start" 
										endAccessor="end" 
										views={views} 
										components={components} 
										onNavigate={handleNavigate} 
									/>}
								/>
								<Route
									name='newEntry'
									path="/entry"
									element={
										<EntryForm
											entry={blankEntry}
											onSave={handleSaveEntry}
											categories={categories} />
									}
								/>
								<Route
									name='editEntry'
									path='/entry/:id/edit'
									element={
										<EntryForm
											entry={blankEntry}
											onSave={handleSaveEntry}
											categories={categories} />
									}
								/>
								<Route
									name='dayView'
									path="day-view/:initialDate"
									element={<DayView onSave={handleSaveEntry}
										cats={categories} />} />

								{/* <Route
									name='report'
									path="report"
									element={<RangeView />} /> */}
								<Route
									name='report'
									path="report"
									element={<SummaryByWeek />} />

								<Route
									name='callback'
									path='callback'
									element={<Callback />} />

								<Route
									name='profile'
									path='profile'
									element={<Profile />} />

								<Route name='categories' path='categories' element={<Categories />} />
							</Routes>
						</div>
					</div>
				</main>
				<footer className="bg-light py-3 mt-auto text-center">
					<p>&copy; {new Date().getFullYear()} Shawn Wallace. All rights reserved.</p>
				</footer>
			</div>
		</Router>
	);
};

export default App;