import React, { useState, useEffect } from "react";
import moment from 'moment';
import apiService from "../../apiService";

const CategorySummaryByWeek = () => {
	const [summary, setSummary] = useState([]);
	const [start, setStart] = useState(moment().startOf('month'));
	const [end, setEnd] = useState(moment().endOf('month'));

	useEffect(() => {
		fetchSummary();
	}, [start]);

	async function fetchSummary() {
		const data = await apiService.getCategorySummaryReport(start, moment(start).endOf('month'));
		setSummary(data);
	};

	const handleDateIncrement = (increment) => {
		var newStart = moment(start).add(increment,'months');
		var newEnd = moment(newStart).endOf('month');
		setStart(newStart);
		setEnd(newEnd);
	};

	return (
		<div className="container>">
			<div className="row">
				<h3>Category Report</h3>
			</div>
			<div className="row">
				<div className="col">
					<div className="flex-bind">
						From
						<button type='button' className='btn btn-light btn-sm' onClick={() => handleDateIncrement(-1)}>
							<i className='bi bi-arrow-left-square'></i>
						</button>
						{start.format("YYYY-MM-DD")}
						<button type='button' className='btn btn-light btn-sm' onClick={() => handleDateIncrement(1)}>
							<i className='bi bi-arrow-right-square'></i>
						</button> to {end.format("YYYY-MM-DD")}
					</div>
				</div>
			</div>
			<div className="row">
				{summary.length > 0 ? (
					<>
						<table className="table table-striped table-bordered table-sm">
							<thead className="thead-dark">
								<tr>
									<th className="text-center" scope="col">Category</th>
									<th className="text-center" scope="col">Summary</th>
									<th className="text-center" scope="col">Total</th>
								</tr>
							</thead>
							<tbody>
								{summary.map((value, index) => (
									<>
										{index > 0 && value.week !== summary[index - 1].week && (
											<tr>
												<td className="table-primary text-start" colSpan="3">({value.week}) {moment(value.firstDayOfWeek).format("YYYY-MM-DD")}</td>
											</tr>)
										}
										{index === 0 && (
											<tr>
												<td className="table-primary text-start" colSpan="3">({value.week}) {moment(value.firstDayOfWeek).format("YYYY-MM-DD")}</td>
											</tr>
										)}
										<tr key={index}>
											<td className="text-start text-nowrap">{value.fullName}</td>
											<td className="text-start">{value.summary}</td>
											<td className="text-end">
												{value.totalHours > 0 && value.totalHours.toFixed(2)}
											</td>
										</tr>
									</>
								))}
							</tbody>
						</table>
					</>) : (
					<div className="row"><p>No entries.</p></div>
				)}
			</div >
		</div >
	);
};

export default CategorySummaryByWeek;