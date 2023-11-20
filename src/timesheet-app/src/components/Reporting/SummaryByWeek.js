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
						<div className="row">
							<div className="col">&nbsp;</div>
							<div className="col">Total</div>
							<div className="col">Utilized Total</div>
							<div className="col">Meeting</div>
							<div className="col">Business Development</div>
						</div>
						{summary.map((value, index) => (
							<div className="row" key={index}>
								<div className="col">{moment(value.firstDayOfWeek).format("YYYY-MM-DD")}</div>
								<div className="col float-right">{value.totalHours.toFixed(2)}</div>
								<div className="col float-right">{value.utilizedHours.toFixed(2)}</div>
								<div className="col float-right">{value.meetingHours.toFixed(2)}</div>
								<div className="col float-right">{value.businessDevelopmentHours.toFixed(2)}</div>
							</div>
						))}
					</>) : (
					<div className="row"><p>No entries.</p></div>
				)}
			</div >
		</div >
	);
};

export default SummaryByWeek;