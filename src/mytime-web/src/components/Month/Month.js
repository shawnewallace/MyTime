import React from 'react';
// import { propTypes } from 'react-bootstrap/esm/Image';
import PropTypes from 'prop-types';
import { format } from 'date-fns';
import addMonths from 'date-fns/addMonths';
import addDays from 'date-fns/addDays';
import subMonths from 'date-fns/subMonths';
import startOfWeek from 'date-fns/startOfWeek';
import endOfWeek from 'date-fns/endOfWeek';
import startOfMonth from 'date-fns/startOfMonth';
import endOfMonth from 'date-fns/endOfMonth';
// import isSameMonth from 'date-fns/isSameMonth'
// import isSameDay from 'date-fns/isSameDay'
import styles from './Month.module.css';
// import Day from '../Day/Day'


class Month extends React.Component {
	constructor(props) {
		super(props);

		this.state = {
			currentMonth: new Date(),
			selectedDate: new Date()
		};
	}

	renderHeader() {
		const dateFormat = "MMMM yyyy";

		return (
			<div className="header row flex-middle">
				<div className="col col-start">
					<div className="icon" onClick={this.prevMonth}>
						chevron_left
					</div>
				</div>
				<div className="col col-center">
					<span>
						{format(this.state.currentMonth, dateFormat)}
					</span>
				</div>
				<div className="col col-end" onClick={this.nextMonth}>
					<div className="icon">
						chevron_right
					</div>
				</div>
			</div>
		);
	}

	renderDays() {
		const dateFormat = "EEEE";
		const days = [];

		let startDate = startOfWeek(this.state.currentMonth);

		for (let i = 0; i < 7; i++) {
			days.push(
				<th>
				<div className="col col-center" key={i}>
					{format(addDays(startDate, i), dateFormat)}
				</div>
				</th>
			);
		}

		return <div className="days row">{days}</div>
	}

	renderCells() {
		const { currentMonth, selectedDate } = this.state;
		const monthStart = startOfMonth(currentMonth);
		const monthEnd = endOfMonth(monthStart);
		const startDate = startOfWeek(monthStart);
		const endDate = endOfWeek(monthEnd);

		const dateFormat = "d";
		const rows = [];

		let days = [];
		let day = startDate;
		let formattedDate = "";

		while (day <= endDate) {
			for (let i = 0; i < 7; i++) {
				formattedDate = format(day, dateFormat);
				// const cloneDay = day;
				days.push(
					// <div className={`col cell ${!isSameMonth(day, monthStart)
					// 		? "disabled"
					// 		: isSameDay(day, selectedDate) ? "selected" : ""
					// 	}`}
					// 	key={day}>
					// 	<span className="number">{formattedDate}</span>
					// 	{/* <Day day={day} /> */}
					// 	{/* <span className="bg">{formattedDate}</span> */}
					// </div>
					<td>{formattedDate}</td>
				);
				day = addDays(day, 1);
			}
			rows.push(
				// <div className='row' key={day}>
				<tr>
					{days}
				</tr>
				// </div>
			);
			days = [];
		}

		return <div className="body">{rows}</div>
	}

	// onDateClick = day => { day = 10 }

	nextMonth = () => {
		this.setState({
			currentMonth: addMonths(this.state.currentMonth, 1)
		});
	};

	prevMonth = () => {
		this.setState({
			currentMonth: subMonths(this.state.currentMonth, 1)
		});
	};

	render() {
		return (
			<div className={styles.Month}>
				{this.renderHeader()}
				<table>
					<tr>
						{this.renderDays()}
					</tr>
					{this.renderCells()}
				</table>
			</div>
		);
	}
}

Month.propTypes = {
	label: PropTypes.string
};

Month.defaultProps = {};

export default Month;
