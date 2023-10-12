import React, { useEffect, useState } from "react";
import apiService from '../../apiService';

const Categories = () => {

	const [categories, setCategories] = useState([]);

	useEffect(() => {
		fetchCategories();
	});

	const fetchCategories = async () => {
		console.log("fetching categories");
		try {
			const data = await apiService.getCategories();
			setCategories(data);

		} catch (error) {
			console.error(`Error Categories`, error);
		}
	};

	return (
		<>
			<div className="row">
				<h1>Categories</h1>
			</div>
			{categories.map((category, index) => (
				<div key={category.name} className="row">
					<div className="col">{category.name}</div>
				</div>
			))}
		</>
	)
};

export default Categories;