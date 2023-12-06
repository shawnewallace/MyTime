import React from 'react';

const CategorySummaryComponent = ({ categorySummary }) => {
	if (categorySummary.length === 0) {
		return <></>;
	}

	return (
		<>
			<table className='table table-sm table-striped'>
				<caption>Summary by Category</caption>
				<thead>
					<tr>
						<th>Category</th>
						<th>Description</th>
						<th>Hours</th>
					</tr>
				</thead>
				<tbody>
					{categorySummary.map((entry, index) => (
						<>
							<tr key={index}>
								<td>{entry.name}</td>
								<td>{entry.descriptions}</td>
								<td className='text-end'>{entry.total.toFixed(2)}</td>
							</tr>
						</>
					))}
				</tbody>
			</table>
		</>
	);
}

export default CategorySummaryComponent;