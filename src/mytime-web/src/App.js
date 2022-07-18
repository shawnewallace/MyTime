import React from 'react';
import Nav from './components/Nav/Nav'
import Month from './components/Month/Month'
import Day from './components/Day/Day'
import './App.css';

function App() {
	const today = new Date();
	const entries = [
		{ id: 0, description: "zero", duration: 0.75, billable: true },
		{ id: 1, description: "one", duration: 5.25, billable: true },
		{ id: 2, description: "two", duration: 4.0, billable: false },
		{ id: 3, description: "three", duration: 0.25, billable: false },
		{ id: 4, description: "four", duration: 1.0, billable: true },
	];

	return (
		<div className="App">
			{process.env.REACT_APP_API_URL}
			<Nav />
			<Month currentMonth={today} />
			<Day day={today} entries={entries} />
		</div>
	);
}

export default App;
