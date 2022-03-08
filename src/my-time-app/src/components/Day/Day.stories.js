/* eslint-disable */
import Day from './Day';

export default {
	title: "Day",
};

const lines = [
	{ Id: 0, Description: "Zero", Duration: 2.25, Billable: false },
	{ Id:1, Description:"One", Duration:3, Billable:true },
	{ Id:2, Description:"Two", Duration:1.5, Billable:false },
	{ Id: 3, Description: "Three", Duration: 1, Billable: true },
]

export const Default = () => <Day label="24" entries={lines} />;

Default.story = {
	name: 'default'
};
