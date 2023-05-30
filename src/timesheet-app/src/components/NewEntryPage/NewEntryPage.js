import React, { useState } from 'react';

const NewEntryPage = ({ onSave, categories }) => {
	const [description, setDescription] = useState('');
	const [category, setCategory] = useState('');
	const [duration, setDuration] = useState('');
	const [billable, setBillable] = useState(false);

	const handleSubmit = (e) => {
		e.preventDefault();

		// Create an entry object with the form data
		const entry = {
			description,
			category,
			duration,
			billable,
		};

		onSave(entry); // Pass the entry to the onSave callback

		// Reset form fields
		setDescription('');
		setCategory('');
		setDuration('');
		setBillable(false);
	};

	return (
		<div>
			<h2>Entry Form</h2>
			<form onSubmit={handleSubmit}>
				<label>
					Description:
					<input
						type="text"
						value={description}
						onChange={(e) => setDescription(e.target.value)}
					/>
				</label>
				<br />
				<label>
					Category:
					<select value={category} onChange={(e) => setCategory(e.target.value)}>
						<option value="">Select category</option>
						{categories.map((cat) => (
							<option value={cat.name} key={cat.name}>
								{cat.name}
							</option>
						))}
					</select>
				</label>
				<br />
				<label>
					Duration:
					<input
						type="text"
						value={duration}
						onChange={(e) => setDuration(e.target.value)}
					/>
				</label>
				<br />
				<label>
					Billable:
					<input
						type="checkbox"
						checked={billable}
						onChange={(e) => setBillable(e.target.checked)}
					/>
				</label>
				<br />
				<button type="submit">Save Entry</button>
			</form>
		</div>
	);
};

export default NewEntryPage;
