import React, { useState, useEffect } from "react";
import moment from 'moment';
import apiService from "../../apiService";

const CategorySummaryByWeek = () => {
	const [summary, setSummary] = useState([]);
	const [start, setStart] = useState(moment().startOf('week'));
	const [end, setEnd] = useState(moment().endOf('week'));

	useEffect(() => {
		fetchSummary();
	}, [start]);

	async function fetchSummary() {
		const data = await apiService.getDaySummaryReport(start, moment(start).endOf('week'));
		setSummary(data);
	};

	const handleDateIncrement = (increment) => {
		var newStart = moment(start).add(increment,'weeks');
		var newEnd = moment(newStart).endOf('week');
		setStart(newStart);
		setEnd(newEnd);
	};

	return (
		<div className="container>">
			<div className="row">
				<h3>Weekly Summary By Day (to help with time entry)</h3>
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
									<th className="text-center" scope="col">Date</th>
									<th className="text-center" scope="col">Category</th>
									<th className="text-center" scope="col">Notes</th>
									<th className="text-center" scope="col">Total</th>
								</tr>
							</thead>
							<tbody>
								{summary.map((value, index) => (
									<>
										{index > 0 && value.onDate !== summary[index - 1].onDate && (
											<tr>
												<td className="table-primary text-start" colSpan="4">{moment(value.onDate).format("YYYY-MM-DD")}</td>
											</tr>)
										}
										{index === 0 && (
											<tr>
												<td className="table-primary text-start" colSpan="4">{moment(value.onDate).format("YYYY-MM-DD")}</td>
											</tr>
										)}
										<tr key={index}>
											<td>&nbsp;</td>
											<td className="text-start">{value.fullName}</td>
											<td className="text-start">
												{value.description.split('\r').map((item, key) => {
													return <span key={key}>{item}<br /></span>
												})}
											</td>
											<td className="text-end">
												{value.duration > 0 && value.duration.toFixed(2)}
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