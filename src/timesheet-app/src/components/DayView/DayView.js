import React, { useState, useEffect } from 'react';
import moment from 'moment';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import apiService from '../../apiService';
import { useParams, Link } from "react-router-dom";

const DayView = ({ onSave, cats }) => {
	let params = useParams();
	let initialDate = params.initialDate;
	const [entries, setEntries] = useState([]);
	const [selectedDate, setSelectedDate] = useState(initialDate ? new Date(initialDate) : new Date())
	// const [categories] = useState(cats);
	const [billable, setBillable] = useState(0);
	const [total, setTotal] = useState(0);

	useEffect(() => {
		setSelectedDate(selectedDate);
		fetchEntries(selectedDate);
	}, [selectedDate]);

	async function fetchEntries(day) {
		const data = await apiService.getEntriesForDay(day);
		setEntries(data.entries);
		setBillable(data.utilizedTotal);
		setTotal(data.total);
	}

	// const handleSaveEntry = (entry) => {
	// 	onSave(entry);
	// 	fetchEntries(selectedDate);
	// };

	const handleDateChange = (date) => {
		setSelectedDate(date);
		fetchEntries(date);
	};

	const visibleDate = moment(selectedDate).format('YYYY-MM-DD');

	return (
		<div className="container">
			<div className='row'>
				<h3>Entries for: <DatePicker selected={selectedDate} onChange={handleDateChange} /></h3>
			</div>

			{/* <div className='row'>
				<div className="col-sm-7">
					<NewEntryPage initialDate={selectedDate} onSave={handleSaveEntry} categories={categories} />
				</div>
			</div> */}

			<div>
				{entries.length > 0 ? (
					<table className="table table-striped">
						<thead>
							<tr>
								<th></th>
								<th></th>
								<th>{total.toFixed(2)}</th>
								<th>{billable.toFixed(2)}</th>
								<th></th>
							</tr>
							<tr>
								<th>Description</th>
								<th>Category</th>
								<th>Duration</th>
								<th>Billable</th>
								<th>Notes</th>
							</tr>
						</thead>
						<tbody>
							{entries.map((entry, index) => (
								<tr key={entry.id}>
									<td>
										<Link className="link" to={`/entry/${entry.id}/edit`}>
										{entry.description}
										</Link>
									</td>
									<td>{entry.category === "NULL" ? "" : entry.category}</td>
									<td>{entry.duration.toFixed(2)}</td>
									<td>{entry.isUtilization ? 'Yes' : 'No'}</td>
									<td>{entry.notes}</td>
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