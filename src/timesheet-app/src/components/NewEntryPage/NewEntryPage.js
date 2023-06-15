import React, { useState, useEffect } from 'react';
import DatePicker from 'react-datepicker';
import CreatableSelect from 'react-select/creatable';
import { useParams } from "react-router-dom";
import apiService from '../../apiService';


const NewEntryPage = ({ initialDate, onSave, categories }) => {
	const params = useParams();
	const entryId = params.entryId;

	const [onDate, setOnDate] = useState(initialDate ? new Date(initialDate) : new Date())
	const [description, setDescription] = useState('');
	const [category, setCategory] = useState('');
	const [duration, setDuration] = useState(0);
	const [billable, setBillable] = useState(false);
	const [notes, setNotes] = useState('');

	const formattedCategoryOptions = categories.map((item) => ({
		value: item.name,
		label: item.name
	}));

	useEffect(() => {
		const fetchEntryDetails = async () => {
			try {
				const entry = await apiService.getEntryById(entryId);

				console.log(entry.onDate);

				setOnDate(entry.onDate);

				setDescription(entry.description);
				setCategory(entry.category);
				setDuration(entry.duration);
				setBillable(entry.isUtilization);
				setNotes(entry.Notes);
			}
			catch (error) {
				console.error('Error fetching entry details:', error);
			}
		};

		fetchEntryDetails();
	}, [entryId, onDate]);

	const handleSubmit = (e) => {
		e.preventDefault();

		// Create an entry object with the form data
		const entry = {
			onDate,
			description,
			category,
			duration,
			billable,
			notes
		};

		onSave(entry); // Pass the entry to the onSave callback

		resetForm();
	};

	const handleCancel = () => {
		resetForm();
	};

	const resetForm = () => {
		setDescription('');
		setCategory('');
		setDuration(0);
		setNotes('');
		setBillable(false);
	};

	const handleDateChange = (date) => {
		setOnDate(date);
	};

	return (
		<div>
			<form onSubmit={handleSubmit}>
				{entryId}
				<div className='form-group row'>
					<label htmlFor='selectedDate' className='col-sm-4'>Create New Entry for</label>
					<div className='col-sm-7'>
						<DatePicker
							id='selectedDate'
							name='selectedDate'
							className='form-control'
							selected={onDate}
							onChange={handleDateChange}
							required
						/>
					</div>
				</div>
				<div className='form-group row'>
					<label htmlFor='description' className='col-sm-2 col-form-label'>Description</label>
					<div className='col-sm-10'>
						<input
							type="text"
							className='form-control'
							id='description'
							name='description'
							value={description}
							onChange={(e) => setDescription(e.target.value)}
							required
						/>
					</div>
				</div>
				<div className='form-group row'>
					<label htmlFor='category' className='col-sm-2 col-form-label'>Category</label>
					<div className='col-sm-10'>
						<CreatableSelect
							className='form-control'
							id='category'
							name='category'
							value={category}
							onChange={(e) => setCategory(e)}
							options={formattedCategoryOptions}
							isClearable
							isSearcable />
					</div>
				</div>
				<div className='form-group row'>
					<label htmlFor='duration' className='col-sm-2 col-form-label'>Duration</label>
					<div className='col-sm-2'>
						<input
							type="number"
							step="0.25"
							className='form-control'
							id='duration'
							name='duration'
							value={duration}
							onChange={(e) => setDuration(e.target.value)}
							min="0.25"
							max="24"
							required
						/>
					</div>
				</div>
				<div className='form-group row'>
					<div className='col-sm-2'>&nbsp;</div>
					<div className='col-sm-10'>
						<input
							type="checkbox"
							className='form-check-input'
							id='billable'
							name='billable'
							checked={billable}
							onChange={(e) => setBillable(e.target.checked)}
						/>
						<label className='form-check-label' htmlFor='billable'>Billable</label>
					</div>
				</div>
				<div className='form-group row'>
					<label htmlFor='notes' className='col-sm-2 col-form-label'>Notes</label>
					<div className='col-sm-10'>
						<textarea
							className="form-control"
							id="notes"
							name="notes"
							rows={8}
							value={notes}
							onChange={(e) => setNotes(e.target.checked)}
						></textarea>
					</div>
				</div>
				<div className='form-group row'>
					<div className="col-sm-10 offset-sm-2">
						<button type="submit" className='btn btn-primary'>Save Entry</button>
						<button className='btn btn-secondary' onClick={handleCancel}>Cancel</button>
					</div>
				</div>
			</form>
		</div>
	);
};

export default NewEntryPage;
