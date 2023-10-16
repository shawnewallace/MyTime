import React, { useEffect, useState } from "react";
import apiService from '../../apiService';

const Categories = () => {

	const [categories, setCategories] = useState([]);

	useEffect(() => {
		fetchCategories();
	}, []);

	const fetchCategories = async () => {
		console.log("fetching categories");
		try {
			const data = await apiService.getAllCategories();
			setCategories(data);
		} catch (error) {
			console.error(`Error Categories`, error);
		}
	};

	const handleNameChange = (ctl, id) => {
		var category = categories.filter(e => e.id === id)[0];

		category.name = ctl.target.value;

		var updatedCategory = {
			id: category.id,
			name: category.name
		};

		apiService.updateCategory(updatedCategory);

		ctl.value = category.description;
	};

	const handleIsActiveChange = (ctl, id) => {
		var category = categories.filter(e => e.id === id)[0];
		category.isDeleted = !category.isDeleted;

		apiService.toggleCategoryAcvite(id);

		ctl.checked = category.isDeleted;
	};

	return (
		<>
			<div className="container">
				<div className="row">
					<h3>Categories</h3>
				</div>
				<div className="row">
					<div className="col"><b>Name</b></div>
					<div className="col"><b>Is Active</b></div>
				</div>
				{categories.map((category, index) => (
					<div key={category.id} className="row">
						<div className="col">
							<input
								type="text"
								className='form-control form-control-sm'
								id='name'
								name='name'
								defaultValue={category.name}
								onChange={(e) => handleNameChange(e, category.id)}
								required
							/>
						</div>
						<div className="col">
							<input type="checkbox"
								defaultChecked={!category.isDeleted}
								onChange={(e) => { handleIsActiveChange(e, category.id) }}
							/>
						</div>
					</div>
				))}
			</div>
		</>
	)
};

export default Categories;