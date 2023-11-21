import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import LoginButton from '../Buttons/LoginButton';
import LogoutButton from '../Buttons/LogoutButton';

const Navigation = () => {
	let currentDate = new Date();
	const { isAuthenticated } = useAuth0();

	return (
		<nav className="navbar navbar-expand-lg navbar-light bg-light">
			<Link className="navbar-brand ms-3" to="/">My Time</Link>

			<button className="navbar-toggler" type="button" data0-bs-toggle="collapse" data-bw-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
				<span className="navbar-toggler-icon"></span>
			</button>

			<div className="collapse navbar-collapse" id="navbarNav">
				<ul className='navbar-nav me-auto'>
					<li className="nav-item">
						<Link className="nav-link" to="/">Calendar</Link>
					</li>
					<li className="nav-item">
						<Link className="nav-link" to="/entry">New Entry</Link>
					</li>
					<li className="nav-item">
						<Link className="nav-link" to={`/day-view/${currentDate.toISOString()}`}>Day View</Link>
					</li>
					<li className="nav-item dropdown">
						<a className="nav-link dropdown-toggle"
							href="/#"
							id="navbarDropdown"
							role="button"
							data-bs-toggle="dropdown"
							aria-expanded="true">
							Reports
						</a>
						<ul className="dropdown-menu" aria-labelledby='navbarDropdown'>
							<li><a className="dropdown-item" href="/rangeReport">Range Report</a></li>
							<li><a className="dropdown-item" href="/reportSummary">Summary by Week</a></li>
						</ul>
					</li>
					<li className="nav-item">
						<Link className="nav-link" to="/profile">Profile</Link>
					</li>
					<li className="nav-item">
						<Link className="nav-link" to="/categories">Manage Categories</Link>
					</li>
				</ul>

				<div className="navbar-nav me-3">
					{isAuthenticated ? (<LogoutButton />) : (<LoginButton />)}
				</div>
			</div>

		</nav >
	);
};

export default Navigation;