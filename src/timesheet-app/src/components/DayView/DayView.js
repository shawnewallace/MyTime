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

	const handleBillableChange = (ctl, id) => {
		var entry = entries.filter(e => e.id === id)[0];
		entry.isUtilization = !entry.isUtilization;

		var updatedEntry = {
			id: entry.id,
			onDate: entry.onDate,
			billable: entry.isUtilization
		};

		apiService.saveUtilization(updatedEntry);

		ctl.checked = entry.isUtilization;
	};

	const handleDurationChange = (ctl, id) => {
		var entry = entries.filter(e => e.id === id)[0];

		entry.duration = parseFloat(ctl.target.value);

		var updatedEntry = {
			id: entry.id,
			onDate: entry.onDate,
			duration: entry.duration
		};

		apiService.saveDuration(updatedEntry);

		ctl.value = entry.duration.toFixed(2);
	};

	const handleDelete = (id) => {
		apiService.deleteEntry(id);

		let newEntries = entries.filter(e => e.id !== id);
		setEntries(newEntries);
	}

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
								<th>&nbsp;</th>
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
										<div class="btn-group" role="group" aria-label="Basic example">
											<button
												type="button"
												class="btn btn-outline-danger btn-sm"
												onClick={() => handleDelete(entry.id)}><i class="bi bi-trash3"></i></button>
											<Link className="link" class="btn btn-outline-primary btn-sm" to={`/entry/${entry.id}/edit`}>
												<i class="bi bi-pencil"></i>
											</Link>
										</div>
									</td>
									<td>
										{entry.description}
									</td>
									<td>{entry.category === "NULL" ? "" : entry.category}</td>
									<td>
										<input
											type="number"
											step="0.25"
											className='form-control'
											id='duration'
											name='duration'
											defaultValue={entry.duration.toFixed(2)}
											onChange={(e) => handleDurationChange(e, entry.id)}
											min="0.25"
											max="24"
										// required
										/>
									</td>
									<td>
										<input type="checkbox"
											defaultChecked={entry.isUtilization}
											onChange={(e) => { handleBillableChange(e, entry.id) }} />
									</td>
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