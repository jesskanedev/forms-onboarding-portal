import React, { Component } from "react";
import axios from "axios";
import { v4 as uuidv4 } from "uuid";

import "./DocumentDropzone.css";

export class DocumentDropzone extends Component {
  static displayName = DocumentDropzone.name;

  constructor(props) {
    super(props);
    this.state = { runId: uuidv4(), selectedFiles: [], documentProcessingResults: [] };
  }

  trackDocumentAdd = event => {
    this.setState({ selectedFiles: event.target.files });
  };

  sendDocumentsForProcessing = () => {
    const data = new FormData();
    data.append("runId", this.state.runId);

    for (var i = 0; i < this.state.selectedFiles.length; i++) {
      data.append("files", this.state.selectedFiles[i]);
    }

    this.sendDocumentData(data);
  };

  render() {
    return (
      <div>
        <div className="row">
          <div className="col-md-12">
            <form method="post" action="#" id="#">
              <div className="form-group files">
                <label>Upload your files </label>
                <input
                  type="file"
                  className="form-control"
                  multiple
                  onChange={this.trackDocumentAdd}
                />
              </div>
            </form>
          </div>
        </div>
        <div className="row">
          <div className="col-md-12">
            <button
              type="button"
              className="btn btn-success btn-block"
              onClick={this.sendDocumentsForProcessing}
            >
              Go
            </button>
          </div>
        </div>
        <div className="row">
          <div className="col-md-12">
            <ul>
              {this.state.documentProcessingResults.map(result => (
                <li key={result.id}>{result.id} </li>
              ))}
            </ul>
          </div>
        </div>
      </div>
    );
  }

  async sendDocumentData(data) {
    const config = { headers: { "Content-Type": "multipart/form-data" } };

    await axios.post("onboarding/processforms", data, config).then(results => {
      this.setState({ documentProcessingResults: results });
    });
  }
}
