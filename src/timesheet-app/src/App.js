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
import CategorySummaryByDay from './components/Reporting/CategorySummaryByDay'
import apiService from './apiService';
import Callback from './components/Callback/Callback';
import Categories from './components/Categories/Categories';
import Home from "./components/Home/Home";
import 'bootstrap/dist/css/bootstrap.min.css';
import "bootstrap-icons/font/bootstrap-icons.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import './App.css';
import CategorySummaryByWeek from './components/Reporting/CategorySummaryByWeek';
import Login from './components/Authentication/Login';
import Signup from './components/Authentication/Signup';
import Profile from './components/Authentication/Profile';
import { AuthContextProvider } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';

const localizer = momentLocalizer(moment);

const views = {
	month: true,
	// week: true,
	// day: true
};

const App = () => {
	const [events, setEvents] = useState([]);
	const [days, setDays] = useState([]);

	const handleSaveEntry = async (entry) => {
		await apiService.createEntry(entry);
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
				title: value.category,
				start: new Date(value.onDate),
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
				<div>
					<Link to={`/day-view/${date.toISOString()}`}>
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

	return (
		<AuthContextProvider>
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
									<Route name="home" path="/" element={<Home />} />
									<Route name="month" path="/month" element={
										<ProtectedRoute>
											<Calendar
												localizer={localizer}
												events={events}
												startAccessor="start"
												endAccessor="end"
												views={views}
												components={components}
												onNavigate={handleNavigate}
											/>
										</ProtectedRoute>}
									/>
									<Route
										name='newEntry'
										path="/entry"
										element={
											<ProtectedRoute><EntryForm
												entry={blankEntry}
												onSave={handleSaveEntry} />
											</ProtectedRoute>
										}
									/>
									<Route
										name='editEntry'
										path='/entry/:id/edit'
										element={
											<ProtectedRoute>
												<EntryForm
													entry={blankEntry}
													onSave={handleSaveEntry} />
											</ProtectedRoute>
										}
									/>
									<Route
										name='dayView'
										path="day-view/:initialDate"
										element={
											<ProtectedRoute>
												<DayView onSave={handleSaveEntry} />
											</ProtectedRoute>
										} />

									<Route
										name='rangereport'
										path="rangereport"
										element={
											<ProtectedRoute><RangeView /></ProtectedRoute>
										} />

									<Route
										name='reportsummary'
										path="reportsummary"
										element={<ProtectedRoute><SummaryByWeek /></ProtectedRoute>} />

									<Route
										name='categoryreport'
										path="categoryreport"
										element={<ProtectedRoute><CategorySummaryByWeek /></ProtectedRoute>} />

									<Route
										name='dayreport'
										path="dayreport"
										element={<ProtectedRoute><CategorySummaryByDay /></ProtectedRoute>} />

									<Route
										name='callback'
										path='callback'
										element={<ProtectedRoute><Callback /></ProtectedRoute>} />

									<Route name='categories' path='categories' element={<ProtectedRoute><Categories /></ProtectedRoute>} />
									<Route name='login' path="/login" element={<Login></Login>}></Route>
									<Route name='signup' path="/signup" element={<Signup></Signup>}></Route>
									<Route name='profile' path="/profile" element={<ProtectedRoute><Profile></Profile></ProtectedRoute>}></Route>
								</Routes>
							</div>
						</div>
					</main>
					<footer className="bg-light py-3 mt-auto text-center">
						<p>&copy; {new Date().getFullYear()} Shawn Wallace. All rights reserved.</p>
					</footer>
				</div>
			</Router>
		</AuthContextProvider>
	);
};

export default App;