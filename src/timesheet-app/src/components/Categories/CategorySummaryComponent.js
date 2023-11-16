import React from 'react';

const CategorySummaryComponent = ({ categorySummary }) => {
	if (categorySummary.length === 0) {
		return <></>;
	}

	return (
		<>
			{categorySummary.map((entry, index) => (
				<div className="row" key={entry.name}>
					<div className="col">{entry.name} ({entry.descriptions})</div>
					<div className="col">{entry.total.toFixed(2)}</div>
				</div>
			))}
		</>
	);
}

export default CategorySummaryComponent;