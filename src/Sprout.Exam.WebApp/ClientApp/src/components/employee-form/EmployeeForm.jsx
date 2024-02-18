import React from 'react';
import EmployeeTypeComponent from '../employee-type/EmployeeType';

const EmployeeFormComponent = (props) => {
  const {handleChange, fullName, birthDate, typeId, salary, tin} = props;
  return (
    <>
      <div className='form-row'>
        <div className='form-group col-md-6'>
          <label htmlFor='inputFullName4'>Full Name: *</label>
          <input type='text' className='form-control'  id='inputFullName4' onChange={handleChange} name="fullName" value={fullName} placeholder='Full Name' />
        </div>
        <div className='form-group col-md-6'>
          <label htmlFor='inputBirthdate4'>Birthdate: *</label>
          <input type='date' className='form-control' id='inputBirthdate4' onChange={handleChange} name="birthdate" value={birthDate} placeholder='Birthdate' />
        </div>
      </div>
      <div className="form-row">
        <div className='form-group col-md-6'>
          <label htmlFor='inputTin4'>TIN: *</label>
          <input type='text' className='form-control' id='inputTin4' onChange={handleChange} value={tin} name="tin" placeholder='TIN' />
        </div>
        <div className='form-group col-md-6'>
          <EmployeeTypeComponent handleChange={handleChange} value={typeId}></EmployeeTypeComponent>
        </div>
      </div>
      <div className="form-row">
        <div className='form-group col-md-6'>
          <label htmlFor='inputSalary4'>Salary: *</label>
          <input type='number' className='form-control' id='inputSalary4' onChange={handleChange} value={salary} name="salary" placeholder='Salary' />
        </div>
      </div>
    </>
  )
}

export default EmployeeFormComponent;