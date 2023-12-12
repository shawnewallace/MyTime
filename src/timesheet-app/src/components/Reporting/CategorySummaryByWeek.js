import React, { useState, useEffect } from "react";
import moment from 'moment';
import apiService from "../../apiService";

const CategorySummaryByWeek = () => {
	const [summary, setSummary] = useState([]);
	const [start, setStart] = useState(moment().startOf('month').format("YYYY-MM-DD"));
	const [end, setEnd] = useState(moment().endOf('month').format("YYYY-MM-DD"));

	useEffect(() => {
		fetchSummary();
	},[]);

	async function fetchSummary() {
		const data = await apiService.getCategorySummaryReport(start, end);
		setSummary(data);
	};

	return (
		<div className="container>">
			<div className="row">
				<h3>Category Report</h3>
			</div>
			<div className="row">
				From {start} to {end}
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
										{index > 0 && value.week !== summary[index -1].week && (
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