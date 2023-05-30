import React, { useState, useEffect } from 'react';
import moment from 'moment';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import NewEntryPage from '../NewEntryPage/NewEntryPage';
import apiService from '../../apiService';

const DayView = () => {
	const [entries, setEntries] = useState([]);
	const [selectedDate, setSelectedDate] = useState(new Date());
	const [categories, setCategories] = useState([]);

	useEffect(() => {
		fetchCategories();
		fetchEntries(new Date());
	}, []);

	const fetchCategories = async () => {
		try {
			const data = await apiService.getCategories();
			setCategories(data);
		} catch (error) {
			console.error('Error fetching categories:', error);
		}
	};

	const fetchEntries = async (day) => {
		const data = await apiService.getEntriesForDay(day);
		setEntries(data.entries);
}

	const handleSaveEntry = (entry) => {
		setEntries([...entries, entry]);
	};

	const handleDateChange = (date) => {
		setSelectedDate(date);
		fetchEntries(date);
	};

	const visibleDate = moment(selectedDate).format('YYYY-MM-DD');
	const dayEntries = entries;

	return (
		<div>
			<h2>Day View - {visibleDate}</h2>

			<div>
				<DatePicker selected={selectedDate} onChange={handleDateChange} />
			</div>

			<NewEntryPage onSave={handleSaveEntry} categories={categories} />

			<div>
				<h3>Entries for {visibleDate}</h3>
				{dayEntries.length > 0 ? (
					<table>
						<thead>
							<tr>
								<th>Description</th>
								<th>Category</th>
								<th>Duration</th>
								<th>Billable</th>
							</tr>
						</thead>
						<tbody>
							{dayEntries.map((entry, index) => (
								<tr key={entry.id}>
									<td>{entry.description}</td>
									<td>{entry.notes}</td>
									<td>{entry.duration.toFixed(2)}</td>
									<td>{entry.isUtilization ? 'Yes' : 'No'}</td>
								</tr>
							))}
						</tbody>
					</table>
				) : (
					<p>No entries for {visibleDate}</p>
				)}
			</div>
		</div>
	);
};

export default DayView;