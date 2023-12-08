import React, { useEffect, useState } from 'react';
import DatePicker from 'react-datepicker';
import CreatableSelect from 'react-select/creatable';
import { useParams } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import apiService from '../../apiService';


const EntryForm = ({ entry, onSubmit, categories }) => {
	const params = useParams();

	const [id, setId] = useState(params.id || '');
	const [onDate, setOnDate] = useState(entry.date ? new Date(entry.date) : new Date())
	const [description, setDescription] = useState(entry.description || '');
	const [category, setCategory] = useState(entry.categoryId || '');
	const [selectedCategory, setSelectedCategory] = useState(entry.categoryId || '');
	const [duration, setDuration] = useState(entry.duration || 0);
	const [isBillable, setIsBillable] = useState(entry.isBillable || false);
	const [notes, setNotes] = useState(entry.notes || '');

	useEffect(() => {
		if (id === '') return;
		fetchEvent(id);
	}, [id]);

	const navigate = useNavigate();

	const fetchEvent = async (eventId) => {
		console.log("fetching event");
		try {
			const data = await apiService.getEntryById(eventId);
			console.log(data);
			setOnDate(new Date(data.onDate));
			setDescription(data.description);

			setCategory(data.categoryId);
			setSelectedCategory(data.categoryId);

			console.log(selectedCategory);

			setDuration(data.duration);
			setIsBillable(data.isUtilization);
			setNotes(data.notes);
		} catch (error) {
			console.error(`Error fetching event: ${eventId}`, error);
		}
	};

	const handleSubmit = async (event) => {
		event.preventDefault();

		let entry = {
			onDate: onDate,
			description: description,
			category: category,
			duration: duration,
			billable: isBillable,
			notes: notes
		};

		console.log("ENTRY TO BE UPSERTED - ", entry);

		if (id === '') {
			//CREATE
			console.log('CREATE EVENT');
			await apiService.createEntry(entry);
		}
		else {
			//UPDATE
			console.log(`EDIT EVENT ${id}`);
			entry.id = id;
			await apiService.updateEntry(entry);
		}

		navigate(-1);
	};

	const handleDateChange = (date) => {
		setOnDate(date);
	};

	const handleCategoryChange = (newCategory) => {
		var newCategoryId = newCategory.target.value;

		setCategory(newCategoryId);
		setSelectedCategory(newCategoryId);
	};


	const handleCancel = (event) => {
		event.preventDefault();
		// resetForm();
		navigate(-1);
	};

	return (
		<div>
			<form onSubmit={handleSubmit}>
				<div className='form-group row'>
					<label htmlFor='selectedDate' className='col-sm-4'>Date</label>
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
					{category}
					<div className='col-sm-10'>
						<select
							id='category'
							className='form-select'
							aria-label='category select'
							defaultValue={selectedCategory}
							onChange={(e) => handleCategoryChange(e)}
						>
							<option value="">-- Select a Category --</option>
							{categories.map((option) => (
								<option key={option.id} value={option.id}>{option.fullName}</option>
							))}
						</select>
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
					<div className='col-sm-2'>
						<input
							type="checkbox"
							className='form-check-input'
							id='billable'
							name='billable'
							checked={isBillable}
							onChange={(e) => setIsBillable(e.target.checked)}
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
							onChange={(e) => setNotes(e.target.value)}
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

export default EntryForm;