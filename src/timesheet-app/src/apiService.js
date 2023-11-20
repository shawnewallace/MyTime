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
	getAllCategories: async () => {
		try {
			const response = await fetch(`${BASE_URL}/categories`);
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error retrieving Categores:', error);
			throw new Error("API request failed");
		}
	},
	getCategoriesInRange: async (start, end) => {
		try {
			const response = await fetch(`${BASE_URL}/categories/between/${start.getFullYear()}-${start.getMonth() + 1}-${start.getDate()}/${end.getFullYear()}-${end.getMonth() + 1}-${end.getDate()}`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				}
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	createCategory: async (category) => {
		let newCategory = {
			name: category
		};

		console.log("creating category", newCategory);

		var jsonEntry = JSON.stringify(newCategory);

		try {
			const response = await fetch(`${BASE_URL}/category`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	updateCategory: async (entry) => {
		let entryPayload = {
			Name: entry.name
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/category/${entry.id}`, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	toggleCategoryAcvite: async (id) => {
		try {
			await fetch(`${BASE_URL}/category/${id}/toggle-active`, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
				},
			});
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
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
	getEntryById: async (id) => {
		const response = await fetch(`${BASE_URL}/entry/${id}`, {
			method: 'GET',
			headers: {
				'Content-Type': 'application/json',
			}
		});
		const data = await response.json();
		return data;
	},
	createEntry: async (entry) => {
		var date = entry.onDate;
		let newEntry = {
			OnDate: date,
			Description: entry.description,
			Category: entry.category,
			Duration: entry.duration,
			IsUtilization: entry.billable,
			Notes: entry.notes
		};

		console.log("creeaging event", newEntry);

		var jsonEntry = JSON.stringify(newEntry);

		try {
			const response = await fetch(`${BASE_URL}/entry`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	updateEntry: async (entry) => {
		var date = entry.onDate;
		let entryPayload = {
			OnDate: date,
			Description: entry.description,
			Category: entry.category,
			Duration: entry.duration,
			IsUtilization: entry.billable,
			Notes: entry.notes
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/entry/${entry.id}`, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	deleteEntry: async (id) => {
		console.log("DELETING " + id);
		try {
			await fetch(`${BASE_URL}/entry/${id}`, {
				method: 'DELETE',
				headers: {
					'Content-Type': 'application/json',
				},
			});
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	saveUtilization: async (entry) => {
		var date = entry.onDate;
		let entryPayload = {
			OnDate: date,
			IsUtilization: entry.billable
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/entry/${entry.id}`, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	saveDuration: async (entry) => {
		var date = entry.onDate;

		let entryPayload = {
			OnDate: date,
			Duration: entry.duration
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/entry/${entry.id}`, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	saveDescription: async (entry) => {
		var date = entry.onDate;

		let entryPayload = {
			OnDate: date,
			Description: entry.description
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/entry/${entry.id}`, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	saveCategory: async (entry) => {
		var date = entry.onDate;

		let entryPayload = {
			OnDate: date,
			Category: entry.category
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/entry/${entry.id}`, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
				},
				body: jsonEntry,
			});
			const data = await response.json();
			return data;
		} catch (error) {
			console.error('API Error:', error);
			throw new Error('API request failed');
		}
	},
	getWeekSummaryReport: async () => {
		try {
			const response = await fetch(`${BASE_URL}/report/week-summary`, {
				method: 'GET',
				headers: {
					'Content-Type': 'application/json',
				},
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