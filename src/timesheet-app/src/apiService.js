import { UserAuth } from "./context/AuthContext";

const BASE_URL = process.env.REACT_APP_API_URL;
// const { user } = UserAuth();

const apiService = {
	// getAccessToken: async () => {
	// 	const token = user.accessToken;
	// 	console.log("token: " + token);
	// 	return token;
	// },
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
	createCategory: async (category, id) => {
		let newCategory = {
			name: category,
			parentId: id
		};

		console.log("creating category", newCategory);

		var jsonEntry = JSON.stringify(newCategory);

		try {
			const response = await fetch(`${BASE_URL}/categories`, {
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
			const response = await fetch(`${BASE_URL}/categories/${entry.id}`, {
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
			await fetch(`${BASE_URL}/categories/${id}/toggle-active`, {
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
		const response = await fetch(`${BASE_URL}/entries/${id}`, {
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
			CategoryId: entry.categoryId,
			Duration: entry.duration,
			IsUtilization: entry.billable,
			Notes: entry.notes
		};

		console.log("creeaging event", newEntry);

		var jsonEntry = JSON.stringify(newEntry);

		try {
			const response = await fetch(`${BASE_URL}/entries`, {
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
			CategoryId: entry.categoryId,
			Duration: entry.duration,
			IsUtilization: entry.billable,
			Notes: entry.notes
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/entries/${entry.id}`, {
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
			await fetch(`${BASE_URL}/entries/${id}`, {
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
			const response = await fetch(`${BASE_URL}/entries/${entry.id}`, {
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
			const response = await fetch(`${BASE_URL}/entries/${entry.id}`, {
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
			const response = await fetch(`${BASE_URL}/entries/${entry.id}`, {
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
			CategoryId: entry.categoryId
		};

		var jsonEntry = JSON.stringify(entryPayload);

		try {
			const response = await fetch(`${BASE_URL}/entries/${entry.id}`, {
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
		let payload = {};
		var jsonEntry = JSON.stringify(payload);

		try {
			const response = await fetch(`${BASE_URL}/report/week-summary`, {
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
	getCategorySummaryReport: async (start, end) => {
		let payload = {};
		var jsonEntry = JSON.stringify(payload);

		try {
			const response = await fetch(`${BASE_URL}/report/category-summary-by-week/${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD") }`, {
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
	getDaySummaryReport: async (start, end) => {
		let payload = {};
		var jsonEntry = JSON.stringify(payload);

		try {
			const response = await fetch(`${BASE_URL}/report/category-summary-by-day/${start.format("YYYY-MM-DD")}/${end.format("YYYY-MM-DD")}`, {
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
};

export default apiService;