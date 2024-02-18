import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import EmployeeFormComponent from '../../components/employee-form/EmployeeForm';

export class EmployeeEdit extends Component {
  static displayName = EmployeeEdit.name;

  constructor(props) {
    super(props);
    this.state = { id: 0, fullName: '', birthdate: '', tin: '', typeId: 1, salary: 0, loading: true, loadingSave: false };
  }

  componentDidMount() {
    this.getEmployee(this.props.match.params.id);
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
        <h1 id="tabelLabel" >Employee Edit</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
    this.setState({ loadingSave: true });
    const token = await authService.getAccessToken();
    this.setState({typeId: parseInt(this.state.typeId)});
    const requestOptions = {
      method: 'PUT',
      headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
      body: JSON.stringify(this.state)
    };
    const response = await fetch('api/employees/' + this.state.id, requestOptions);

    if (response.status === 200) {
      this.setState({ loadingSave: false });
      alert("Employee successfully saved");
      this.props.history.push("/employees/index");
    }
    else {
      alert("There was an error occured.");
    }
  }

  async getEmployee(id) {
    this.setState({ loading: true, loadingSave: false });
    const token = await authService.getAccessToken();
    const response = await fetch('api/employees/' + id, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ id: data.id, fullName: data.fullName, birthdate: data.birthdate, salary: data.salary, tin: data.tin, typeId: data.typeId, loading: false, loadingSave: false });
  }
}
