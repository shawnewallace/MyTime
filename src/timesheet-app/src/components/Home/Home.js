import React from 'react';

const Home = () => (
	<>
		<div className='container-fluid text-center'>
			<div className='card mx-auto' style={{ width: 512 }}>
				<div className='card-header'>
					<img src={`${process.env.PUBLIC_URL}/assets/images/bison-landscape.png`} width={512} height={512} className="card-img-top" alt="Bison Landscape"></img>
				</div>
				<div className='card-body'>
					<h4 className='card-title'>This is MyTime</h4>
					<div className='card-text'>Login and enter your time...</div>
				</div>
			</div>
		</div>
	</>
);

export default Home;