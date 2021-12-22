import React from 'react';
// import PropTypes from 'prop-types';
import styles from './Day.module.css';

const Day = () => (
	<div className={styles.day}>
		Day Component - Shawn
		<div className={StyleSheet.dayRow}>
			<input placeholder='description'></input>
			<input className={styles.dayDuration} value={10.2}></input>
			<input type="checkbox"></input>
		</div>
	</div>
);

Day.propTypes = {};

Day.defaultProps = {};

export default Day;
