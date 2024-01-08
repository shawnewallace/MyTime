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

	const handleNewCategory = () => {
		apiService.createCategory("_New Category_").then((data) => {
			fetchCategories();
		});
	};

	const handleNewChildCategory = (id) => {
		apiService.createCategory("_New CHILD Category_", id).then((data) => {
			fetchCategories();
		});
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

		apiService.toggleCategoryAcvite(id).then((data) => {
			ctl.checked = category.isDeleted;
			fetchCategories();
		});

	};

	return (
		<>
			<div className="container">
				<div className="row">
					<div className="col">
						<h3>Categories
							<div className="btn-group" role="group" aria-label="Basic example">
								<button type='button' className='btn btn-light btn-sm' onClick={() => handleNewCategory()}><i className='bi bi-file-plus'></i></button>
								<button type='button' className='btn btn-light btn-sm' onClick={() => fetchCategories()}><i className='bi bi-arrow-clockwise'></i></button>
							</div>
						</h3>
					</div>
				</div>
				<div className="row">
					<div className="col"><b>Name</b></div>
					<div className="col"><b>Is Active</b></div>
				</div>
				{categories.map((category, index) => (
					<div key={category.id} className="row">
						<div className="col">
							<div className="input-group">
								{category.parentId && (
									<span className="input-group-text" id="basic-addon1">
										<i className="bi bi-diagram-2" data-bs-toggle="tooltip" data-bs-title='Child'></i>
									</span>
								)}
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
						</div>
						<div className="col">
							<input type="checkbox"
								defaultChecked={!category.isDeleted}
								onChange={(e) => { handleIsActiveChange(e, category.id) }}
							/>
						</div>
						<div className="col">
							{!category.parentId && (
								<button type='button' className='btn btn-light btn-sm' onClick={() => handleNewChildCategory(category.id)}>
									<i className='bi bi-file-plus'></i>
								</button>
							)}
						</div>
					</div>
				))}
			</div>
		</>
	)
};

export default Categories;