import React from 'react';
import { Link } from 'react-router-dom';

const Navigation = () => {
	<nav className="navbar navbar-expand-lg navbar-light bg-light">
		<Link className="navbar-brand" to="/">My Time</Link>
		<button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
			<span className="navbar-toggler-icon"></span>
		</button>
		<div className="collapse navbar-collapse" id="navbarNav">
			<ul className="navbar-nav">
				<li className="nav-item"><Link className="nav-link" to="/">Calendar</Link></li>
				<li className="nav-item"><Link className="nav-link" to="/entry">New Entry For Today</Link></li>
				<li className="nav-item"><Link className="nav-link" to="/day-view">Day View</Link></li>
			</ul>
		</div>
	</nav>
};

export default Navigation;