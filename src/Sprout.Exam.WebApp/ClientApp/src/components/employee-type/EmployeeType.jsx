
import React from 'react';

const EmployeeTypeComponent = (props) => {
  const { handleChange, value } = props;

  return(
    <>
      <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
      <select id='inputEmployeeType4' onChange={handleChange} value={value}  name="typeId" className='form-control'>
        <option value={1}>Regular</option>
        <option value={2}>Contractual</option>
      </select>
    </>
  ) 
}

export default EmployeeTypeComponent