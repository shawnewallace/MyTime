import React from "react";

const LogoutButton = () => {

	return (
		<button
			className="btn btn-outline-danger"
			// onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
			>
			Log Out
		</button>
	);
};

export default LogoutButton;