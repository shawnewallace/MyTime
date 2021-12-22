import React from 'react';
// import { propTypes } from 'react-bootstrap/esm/Image';
import PropTypes from 'prop-types';
import styles from './Month.module.css';
import Day from '../Day/Day'

const Month = (props) => (
  <div className={styles.Month}>
    {props.label}
		
		for (var i = 0; i < 10; i++)
		{
			
		}

		<Day />
  </div>
);

Month.propTypes = {
	label: PropTypes.string
};

Month.defaultProps = {};

export default Month;
