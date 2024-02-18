import React, { Component, useRef } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import EmployeeFormComponent from '../../components/employee-form/EmployeeForm';

export class EmployeeCreate extends Component {
  static displayName = EmployeeCreate.name;

  constructor(props) {
    super(props);
    this.state = { fullName: '', birthdate: '', tin: '', typeId: 1, salary: 0, error: undefined, loading: false, loadingSave: false };
  }

  componentDidMount() {
  }

  handleChange(event) {
    this.setState({ [event.target.name]: event.target.value });
  }

  handleSubmit(e) {
    e.preventDefault();
    if (window.confirm("Are you sure you want to save?")) {
      this.saveEmployee();
    }
  }

  render() {

    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <div>
        <form>
          <EmployeeFormComponent
            handleChange={this.handleChange.bind(this)}
            typeId={this.state.typeId}
            birthDate={this.state.birthdate}
            fullName={this.state.fullName}
            tin={this.state.tin}
            salary={this.state.salary}
          >
          </EmployeeFormComponent>
          <button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingSave} className="btn btn-primary mr-2">{this.state.loadingSave ? "Loading..." : "Save"}</button>
          <button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
        </form>
      </div>;

    return (
      <div>
        <h1 id="tabelLabel" >Employee Create</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {

    if (this.state.birthdate === '' ||
      this.state.birthdate === null ||
      this.state.birthdate === undefined) {
      alert('Invalid birthdate.');
      return;
    }


    this.setState({ loadingSave: true, error: undefined });
    const token = await authService.getAccessToken();
    this.setState({typeId: parseInt(this.state.typeId)});
    const requestOptions = {
      method: 'POST',
      headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
      body: JSON.stringify(this.state)
    };
    const response = await fetch('api/employees', requestOptions);

    if (response.status === 201) {
      this.setState({ loadingSave: false });
      alert("Employee successfully saved");
      this.props.history.push("/employees/index");
    }
    else {
      var result = await response.json();
      alert(JSON.stringify(result));
      this.setState({ fullName: '', birthdate: '', tin: '', typeId: 1, salary: 0, error: undefined, loading: false, loadingSave: false });
    }
  }

}
