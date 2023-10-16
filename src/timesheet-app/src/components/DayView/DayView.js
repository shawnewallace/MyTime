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
	const [billable, setBillable] = useState(0);
	const [total, setTotal] = useState(0);
	const [categorySummary, setCategorySummary] = useState([]);

	useEffect(() => {
		setSelectedDate(selectedDate);
		fetchEntries(selectedDate);
		fetchCategories(selectedDate);
	}, [selectedDate]);

	async function fetchEntries(day) {
		const data = await apiService.getEntriesForDay(day);
		setEntries(data.entries);
		setBillable(data.utilizedTotal);
		setTotal(data.total);
	};

	async function fetchCategories(day) {
		const data = await apiService.getCategoriesInRange(day, day)
		setCategorySummary(data);
	};

	const handleDateChange = (date) => {
		setSelectedDate(date);
		fetchEntries(date);
	};

	const handleRefreshEntry = () => {
		fetchEntries(selectedDate);
		fetchCategories(selectedDate);
	};

	const handleDateIncrement = (increment) => {
		handleDateChange(moment(selectedDate).add(increment, 'days').toDate());
	};

	const handleNewEntry = () => {
		var entry = {
			onDate: selectedDate,
			description: 'new entry',
			duration: 0.25
		};

		apiService.createEntry(entry).then((data) => {
			var newEntry = {
				id: data.id,
				onDate: data.onDate,
				description: data.description,
				duration: data.duration,
				isUtilization: data.billable,
				category: data.category,
				notes: data.notes
			};

			let newEntries = [...entries, newEntry];
			setEntries(newEntries);
		});
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

	const handleDescriptionChange = (ctl, id) => {
		var entry = entries.filter(e => e.id === id)[0];

		entry.description = ctl.target.value;

		var updatedEntry = {
			id: entry.id,
			onDate: entry.onDate,
			description: entry.description
		};

		apiService.saveDescription(updatedEntry);

		ctl.value = entry.description;
	};

	const handleCategoryChangeDD = (ctl, id) => {
		var entry = entries.filter(e => e.id === id)[0];

		entry.category = ctl.target.value;

		var updatedEntry = {
			id: entry.id,
			onDate: entry.onDate,
			category: entry.category
		};

		apiService.saveCategory(updatedEntry);

		ctl.value = entry.category;
	};

	const handleDelete = (id) => {
		apiService.deleteEntry(id);

		let newEntries = entries.filter(e => e.id !== id);
		setEntries(newEntries);
	}

	const visibleDate = moment(selectedDate).format('YYYY-MM-DD');

	return (
		<>
			<div className="container">
				<div className='row'>
					<h3>Entries for:</h3>
				</div>
				<div className='row row-cols-2' style={{ display: "flex", maxWidth: "250px" }}>
					<div className='col'>
						<DatePicker selected={selectedDate} onChange={handleDateChange} />
					</div>
					<div className='col'>
						<div className="btn-group" role="group" aria-label="Basic example">
							<button type='button' className='btn btn-light btn-sm' onClick={() => handleDateIncrement(-1)}><i className='bi bi-arrow-left-square'></i></button>
							<button type='button' className='btn btn-light btn-sm' onClick={() => handleDateIncrement(1)}><i className='bi bi-arrow-right-square'></i></button>
							<button type='button' className='btn btn-light btn-sm' onClick={() => handleDateChange(new Date())}><i className='bi bi-calendar-check'></i></button>
							<button type='button' className='btn btn-light btn-sm' onClick={() => handleNewEntry()}><i className='bi bi-calendar-plus'></i></button>
							<button type='button' className='btn btn-light btn-sm' onClick={() => handleRefreshEntry()}><i className='bi bi-arrow-clockwise'></i></button>
						</div>
					</div>
				</div>
				<div className='row'>
					{entries.length > 0 ? (
						<table className="table table-striped">
							<thead>
								<tr>
									<th></th>
									<th></th>
									<th></th>
									<th>{total.toFixed(2)}</th>
									<th>{billable.toFixed(2)}</th>
									<td></td>
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
											<div className="btn-group" role="group" aria-label="Basic example">
												<button
													type="button"
													className="btn btn-outline-danger btn-sm"
													onClick={() => handleDelete(entry.id)}>
													<i className="bi bi-trash3"></i>
												</button>
												<Link className="link btn btn-outline-primary btn-sm" to={`/entry/${entry.id}/edit`}>
													<i className="bi bi-pencil"></i>
												</Link>
											</div>
										</td>
										<td>
											<input
												type="text"
												className='form-control'
												id='description'
												name='description'
												defaultValue={entry.description}
												onChange={(e) => handleDescriptionChange(e, entry.id)}
												required
											/>
										</td>
										<td>
											<select
												className='form-select'
												aria-label='category select'
												defaultValue={entry.category}
												onChange={(e) => handleCategoryChangeDD(e, entry.id)}>
												<option value="">-- Select a Category --</option>
												{cats.map((option) => (
													<option key={option.name} value={option.name}>{option.name}</option>
												))}
											</select>
										</td>
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

			<hr />

			<div className="container">
				{categorySummary.length > 0 ? (
					<>
						{categorySummary.map((entry, index) => (
							<div className="row">
								<div className="col">{entry.name}</div>
								<div className="col">{entry.total.toFixed(2)}</div>
							</div>
						))}
					</>
				) : (
					<></>
				)}
			</div>
		</>
	);
};

export default DayView;