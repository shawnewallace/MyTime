import React, { useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react';

const Callback = () => {

	const { handleRedirectCallback } = useAuth0();

	// useEffect(() => {
	// 	const processAuth = async () => {
	// 		await handleRedirectCallback();
	// 		window.location.href = '/';
	// 	};
	// 	processAuth();
	// }, [handleRedirectCallback]);

	return <div>Loading...</div>;
};

export default Callback;