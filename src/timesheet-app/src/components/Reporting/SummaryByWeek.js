import React, { useState, useEffect } from "react";
import moment from 'moment';
import apiService from "../../apiService";

const SummaryByWeek = () => {
	const [summary, setSummary] = useState([]);

	useEffect(() => {
		fetchSummary();
	}, []);

	async function fetchSummary() {
		const data = await apiService.getWeekSummaryReport();
		setSummary(data);
	};


	return (
		<div className="container>">
			<div className="row">
				<h3>Summary By Week</h3>
			</div>
			<div className="row">
				{summary.length > 0 ? (
					<>
						<table className="table table-striped table-bordered table-sm">
							<thead className="thead-dark">
								<tr>
									<th className="text-center" scope="col">&nbsp;</th>
									<th className="text-center" scope="col">Week Of</th>
									<th className="text-center" scope="col">Total</th>
									<th className="text-center" scope="col">Utilized</th>
									<th className="text-center" scope="col">Meetings</th>
									<th className="text-center" scope="col">Business Development</th>
								</tr>
							</thead>
							<tbody>
								{summary.map((value, index) => (
									<tr key={index}>
										<th className="text-center" scope="row">{value.weekNumber}</th>
										<th className="text-center">{moment(value.firstDayOfWeek).format("YYYY-MM-DD")}</th>
										<td className="text-end">
											{value.totalHours > 0 && value.totalHours.toFixed(2)}
										</td>
										<td className="text-end">
											{value.utilizedHours > 0 ? (
												<>
													{value.utilizedHours.toFixed(2)}&nbsp;
													({value.utilizedPercentage.toFixed(2)}%)
												</>) : (<></>)}
										</td>
										<td className="text-end">
											{value.meetingHours > 0 ? (
												<>
													{value.meetingHours.toFixed(2)}&nbsp;
													({value.meetingHoursPercentage.toFixed(2)}%)
												</>) : (<></>)}
										</td>
										<td className="text-end">
											{value.businessDevelopmentHours > 0 ? (
												<>
													{value.businessDevelopmentHours.toFixed(2)}&nbsp;
													({value.businessDevelopmentHoursPercentage.toFixed(2)}%)
												</>) : (<></>)}
										</td>
									</tr>
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

export default SummaryByWeek;