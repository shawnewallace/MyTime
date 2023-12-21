import React from "react";
import { useNavigate } from "react-router-dom";
import { auth } from "../../firebase";
import { signOut } from "firebase/auth";

const LogoutButton = () => {

	const navigate = useNavigate();

	const logoutUser = async (e) => {
		e.preventDefault();

		await signOut(auth).then(() => {
			navigate("/");
		});
	}

	return (
		<button
			className="btn btn-outline-danger"
			onClick={(e) => logoutUser(e)}>
			Log Out
		</button>
	);
};

export default LogoutButton;