import React from 'react';
// import PropTypes from 'prop-types';
import { format } from 'date-fns';
import styles from './Day.module.css';

const Day = (props) => {

	if (!props.entries) {
		props.entries = [{ id: 0, description: "zero", duration: 0.75, billable: true }]
	}

	const handleChange = () => {
	
	};

	const formattedEntries = props.entries.map((row) => {
		return (
			<div className={StyleSheet.dayRow} key={row.id}>
				<input className={StyleSheet.formControlXs} placeholder='description' value={row.description}></input>
				<input className={styles.dayDuration} value={row.duration}></input>
				<input type="checkbox" checked={row.billable} onChange={handleChange}></input>
			</div>
		)
	});

	return (
		<div className={styles.day}>
			[{format(props.day, "MM/dd/yyyy")}]

			{formattedEntries}

		</div>
	)
};

Day.propTypes = {};

Day.defaultProps = {};

export default Day;
