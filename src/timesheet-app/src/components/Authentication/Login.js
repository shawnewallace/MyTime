import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { UserAuth } from "../../context/AuthContext";

const Login = () => {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [notice, setNotice] = useState("");
	const navigate = useNavigate();
	const { signIn } = UserAuth();

	const handleSubmit = async (e) => {

		e.preventDefault();

		try {
			setNotice("Authenticating...");
			await signIn(email, password);
			setNotice("Success");
			navigate("/profile");
		} catch (error) {
			console.log(error.message);
			setNotice(error.message);
		}
	}

	return (
		<div className="container">
			<div className="row justify-content-center">
				<form className="col-md-4 mt-3 pt-3 pb-3" onSubmit={handleSubmit}>
					{"" !== notice &&
						<div className="alert alert-warning" role="alert">
							{notice}
						</div>
					}
					<div className="form-floating mb-3">
						<input type="email" 
						       className="form-control" 
									 id="exampleInputEmail1" 
									 aria-describedby="emailHelp" 
									 placeholder="name@example.com" 
									//  value={email} 
									 onChange={(e) => setEmail(e.target.value)}>
						</input>
						<label htmlFor="exampleInputEmail1" className="form-label">Email address</label>
					</div>
					<div className="form-floating mb-3">
						<input type="password" 
						       className="form-control" 
									 id="exampleInputPassword1" 
									 placeholder="Password" 
									//  value={password} 
									 onChange={(e) => setPassword(e.target.value)}>
						</input>
						<label htmlFor="exampleInputPassword1" className="form-label">Password</label>
					</div>
					<div className="d-grid">
						<button type="submit" className="btn btn-primary pt-3 pb-3">Submit</button>
					</div>
					<div className="mt-3 text-center">
						<span>Need to sign up for an account? <Link to="/signup">Click here.</Link></span>
					</div>
				</form>
			</div>
		</div>
	);
}

export default Login;