import {
	useState,
	useCallback,
} from 'react';

export const getEntries = async (options) => {
	// const response = await fetch('https://localhost:5001/entryday/between/2022-02-01/2022-02-07', options);
	const response = await fetch('https://localhost:5001/entryday/between/2022-02-01/2022-02-07', {
		...options,
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		// body: JSON.stringify(data),
	});
	const data = await response.json();

	return data;
};

export const useGetEntries = () => {
	const [isLoading, setIsLoading] = useState(false);
	const [error, setError] = useState(null);
	const [data, setData] = useState(null);

	const execute = async (options = {}) => {
		try {
			setIsLoading(true);
			const todos = await getEntries(options);
			setData(todos);
			return todos;
		} catch (e) {
			setError(e);
			setIsLoading(false);
			throw e;
		}
	};

	return {
		isLoading,
		error,
		data,
		execute: useCallback(execute, []),
	};
};

