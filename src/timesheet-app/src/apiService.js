const BASE_URL = process.env.REACT_APP_API_URL;

const apiService = {
	getCategories: async () => {
		try {
			const response = await fetch(`${BASE_URL}/categories/lookup`);
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error retrieving Categores:', error);
			throw new Error("API request failed");
		}
	},
	getEntries: async (start, end) => {
		try {
			const response = await fetch(`${BASE_URL}/entries/between/${start.getFullYear()}-${start.getMonth() + 1}-${start.getDate()}/${end.getFullYear()}-${end.getMonth() + 1}-${end.getDate()}`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				// body: JSON.stringify(entry),
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	getEntriesForDay: async (start) => {
		try {
			const entry = {
				"year": start.getFullYear(),
				"month": start.getMonth() + 1,
				"day": start.getDate()
			}
			const response = await fetch(`${BASE_URL}/day`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify(entry),
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
};

export default apiService;