import React, { Component } from "react";
import { DocumentDropzone } from "./DocumentDropzone";

export class Onboard extends Component {
  static displayName = Onboard.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderOnboardPageContent(forecasts) {
    return (
      <div>
        <DocumentDropzone />
        <br />
        <table className="table table-striped" aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Date</th>
              <th>Temp. (C)</th>
              <th>Temp. (F)</th>
              <th>Summary</th>
            </tr>
          </thead>
          <tbody>
            {forecasts.map(forecast => (
              <tr key={forecast.date}>
                <td>{forecast.date}</td>
                <td>{forecast.temperatureC}</td>
                <td>{forecast.temperatureF}</td>
                <td>{forecast.summary}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      Onboard.renderOnboardPageContent(this.state.forecasts)
    );

    return (
      <div>
        <h1 id="tabelLabel">Onboarding</h1>
        <p>
          Upload your Tax Code form to see this onboarding solution in action!
          More forms will be supported as we continue building out
          functionality.
        </p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch("weatherforecast");
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
