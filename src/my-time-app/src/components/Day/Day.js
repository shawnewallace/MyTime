import React from 'react';
import PropTypes from 'prop-types';
import './Day.css';

const Day = (props) => {

	const handleChange = (evt) => {
		console.log("new value", evt.target.value);
		billableSum++;
	}

	let billableSum = props.entries.reduce(function (prev, current) {
		if (current.Billable) prev += current.Duration
		return prev;
	}, 0);

	var nonBillableSum = props.entries.reduce(function (prev, current) {
		if (!current.Billable) prev += current.Duration
		return prev;
	}, 0);

	var theEntries = props.entries.map(function (entry, index) {
		return (
			<div className="row" key={entry.Id}>
				<div className="col-auto">
					<input className='form-control form-control-sm' type='text' placeholder='description' defaultValue={entry.Description}></input>
				</div>
				<div className="col-auto">
					<input className='form-control form-control-sm' type='number' min="0" max="24.0" step="0.25" defaultValue={entry.Duration} onChange={handleChange}></input>
				</div>
				<div className="col-auto">
					<input className="form-check-input" type="checkbox" defaultChecked={entry.Billable}></input>
				</div>
			</div>)
	});

	return (
		<div className="Day card border-secondary">
			<div className="card-header text-left">
				{props.label}
			</div>
			<div className="card-body text-center">

				{theEntries}

			</div>
			<div className="card-footer">
				<label>{billableSum}</label>(<label>{nonBillableSum}</label>)
			</div>
		</div>
	)
};

Day.propTypes = {
	label: PropTypes.string,
	entries: PropTypes.array
};

Day.defaultProps = {
	// label: '10'
};

export default Day;
