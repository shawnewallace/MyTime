import React from 'react';
import { Link } from 'react-router-dom';

const Navigation = () => {
	let currentDate = new Date();

	return (
		<nav className="navbar navbar-expand-lg navbar-light bg-light">
			<Link className="navbar-brand" to="/">My Time</Link>
			<button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
				<span className="navbar-toggler-icon"></span>
			</button>
			<div className="collapse navbar-collapse" id="navbarNav">
				<ul className="navbar-nav ml-auto">
					<li className="nav-item">
						<Link className="nav-link" to="/">Calendar</Link>
					</li>
					<li className="nav-item">
						<Link className="nav-link" to="/entry">New Entry</Link>
					</li>
					<li className="nav-item">
						<Link className="nav-link" to={`/day-view/${currentDate.toISOString()}`}>Day View</Link>
					</li>
					<li className="nav-item">
						<Link className="nav-link" to="/report">Reporting</Link>
					</li>
				</ul>
			</div>
		</nav>
	);
};

export default Navigation;