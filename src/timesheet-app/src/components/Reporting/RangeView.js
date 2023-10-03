import React, { useState, useEffect } from "react";
import moment from 'moment';
import apiService from "../../apiService";

const RangeView = () => {
	let today = new Date();
	const [entries, setEntries] = useState([]);
	const [selectedFirstDayOfMonth, setSelectedFirstDayOfMonth] = useState(new Date(today.getFullYear(), today.getMonth(), 1));

	const firstDayOfTheMonth = moment(selectedFirstDayOfMonth).format('YYYY-MM-DD');
	const lastDayOfTheMonth = moment(selectedFirstDayOfMonth).endOf('month').format('YYYY-MM-DD');

	useEffect(() => {
		setSelectedFirstDayOfMonth(selectedFirstDayOfMonth);
		fetchEntries(selectedFirstDayOfMonth);
	}, [selectedFirstDayOfMonth]);

	async function fetchEntries() {
		let begin = selectedFirstDayOfMonth;
		let end = moment(selectedFirstDayOfMonth).endOf('month').toDate();

		const data = await apiService.getEntries(begin, end);
		setEntries(data.entries);
		// setBillable(data.utilizedTotal);
		// setTotal(data.total);
	}

	const handleDateChange = (date) => {
		setSelectedFirstDayOfMonth(date);
		fetchEntries();
	};

	const handleDateIncrement = (increment) => {
		let newDate = moment(selectedFirstDayOfMonth).add(increment, 'months').toDate();

alert(newDate);
		handleDateChange(moment(selectedFirstDayOfMonth).add(increment, 'months').toDate());
	};

	return (
		<div className="container">
			<div className='row'>
				<h3>Range View</h3>
			</div>
			<div className="row">
				<div className="col">
					<div className="flex-bind">
						
						Entries for month starting: 
						<button type='button' className='btn btn-light btn-sm' onClick={() => handleDateIncrement(-1)}>
							<i className='bi bi-arrow-left-square'></i>
						</button>
						{firstDayOfTheMonth}
						<button type='button' className='btn btn-light btn-sm' onClick={() => handleDateIncrement(1)}>
							<i className='bi bi-arrow-right-square'></i>
						</button>
						and ending: {lastDayOfTheMonth}
					</div>
				</div>
				<div className="row">
					{entries.length > 0 ? (<></>) : (
						<p>No entries for this month.</p>
					)}
				</div>
			</div>
		</div>
	);
};

export default RangeView;