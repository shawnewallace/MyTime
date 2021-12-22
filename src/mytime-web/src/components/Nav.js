import React, { Component } from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Image from 'react-bootstrap/Image';
import BrandLogo from '../logo.svg';

class Nav extends Component {
	render(){
		return(
			<div>
				<Navbar href="/" className='d-flex p-4 pb-0'>
					<Image src={BrandLogo}
						width="30"
						height="30"
						className="align-top"
						alt="Company Logo"
					/>

					<p className='p-3 pt-0 pb-0 text-dark info'>Your Cart Page</p>
				</Navbar>
			</div>
		);
	}
}

export default Nav;