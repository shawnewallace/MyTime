import React from 'react';
import { useNavigate } from 'react-router-dom';

const LoginButton = () => {
	const navigate = useNavigate();
	return (
		<button
			className="btn btn-outline-primary"
			onClick={() => navigate("/login")}
		>
				Log In
		</button>);
};

export default LoginButton;