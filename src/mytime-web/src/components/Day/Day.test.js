import React from 'react';
import ReactDOM from 'react-dom';
import Day from './Day';

it('It should mount', () => {
  const div = document.createElement('div');
  ReactDOM.render(<Day />, div);
  ReactDOM.unmountComponentAtNode(div);
});