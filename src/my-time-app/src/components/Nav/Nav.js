import React from 'react';
import PropTypes from 'prop-types';
import './Nav.css';

const Nav = () => (
  <div className="Nav">
		<nav className="navbar navbar-dark bg-primary">
			<span className="navbar-brand" href="#">MyTime</span>
		</nav>
  </div>
);

Nav.propTypes = {};

Nav.defaultProps = {};

export default Nav;
