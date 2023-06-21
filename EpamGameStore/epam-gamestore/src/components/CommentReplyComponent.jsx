import React, { Component } from 'react'
import "./ComponentsCSS/CommentReplyComponent.css"

export default class CommentReplyComponent extends Component {
  constructor(props) {
    super(props)
    this.AddReplyRequest = this.AddReplyRequest.bind(this);
    this.CancelCommentEditing = this.CancelCommentEditing.bind(this);
    this.EditCommentaryRequest = this.EditCommentaryRequest.bind(this);
    this.ChangeDeletingState = this.ChangeDeletingState.bind(this);
    this.AddCommentaryRequest = this.AddCommentaryRequest.bind(this);

    this.state = {
      DayDistance: "",
      HourDistance: "",
      MinuteDistance: "",
      Id: this.props.Id,
      ReplyState: "None",
      ReplyText: "",
      CanBeEdited: false,
      CommentaryText: this.props.Text,
      IsBeingDeleted: false
    }
  }


  async EditCommentaryRequest() {
    const response = await fetch("http://localhost:21409/api/comments/editreply?replyId=" + this.state.Id + "&newValue=" + this.state.CommentaryText,
      {
        method: 'POST',
        credentials: 'include',
        headers: {
          'Accept': 'application/json'
        }
      }
    );
    const jsonResult = await response.json()
    this.setState({ ReplyState: "None", ReplyText: "" });
  }

  CancelCommentEditing() {
    this.setState({ ReplyState: "None", CommentaryText: this.props.Text });
  }


  async AddReplyRequest() {
    if (this.state.ReplyText.length > 0) {
      const response = await fetch("http://localhost:21409/api/comments/addreply?parentCommentId=" + this.state.Id + "&value=" + this.state.ReplyText,
        {
          method: 'POST',
          credentials: 'include',
          headers: {
            'Accept': 'application/json'
          }
        }
      );
      const jsonResult = await response.json()
      this.setState({ Replys: [...this.state.Replys, jsonResult[0]] });
      this.setState({ ReplyState: "None", ReplyText: "" });
    }

  }

  async AddCommentaryRequest() {
    fetch('http://localhost:21409/api/comments/addreply?parentCommentId=' + this.props.ParentId + '&value=' + this.state.CommentaryText
      , {
        method: 'POST',
        credentials: 'include',
        headers: {
          'Accept': 'application/json'
        }
      })
  }

  async ChangeDeletingState(state) {
    if (state === true) {
      this.setState({ IsBeingDeleted: true });
      await this.DeleteCommentaryRequest();
    }
    else {
      this.setState({ IsBeingDeleted: false });
      await this.AddCommentaryRequest();
    }
  }

  async DeleteCommentaryRequest() {
    const response = await fetch("http://localhost:21409/api/comments/deletereply?replyId=" + this.state.Id,
      {
        method: 'POST',
        credentials: 'include',
        headers: {
          'Accept': 'application/json'
        }
      }
    );
  }

  async componentDidMount() {
    var date = new Date(this.props.Date);
    const now = new Date().getTime();
    const distance = date.getTime() - now;

    const days = Math.floor(distance / (1000 * 60 * 60 * 24));
    const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));

    this.setState({ DayDistance: Math.abs(days), HourDistance: Math.abs(hours), MinuteDistance: Math.abs(minutes) });
    if (localStorage.getItem("UserName") === this.props.Author || localStorage.getItem("UserRole") === "Admin" || localStorage.getItem("UserRole") === "Manager") {
      this.setState({ CanBeEdited: true });
    }
  }


  render() {
    switch (this.state.ReplyState) {
      case "AddReply":
        return (
          <div className='Reply'>
            <p className='ReplyTopText'>{this.props.Author + "    commented " + this.state.DayDistance + " days ago"}</p>
            <p className='CommentaryText'>{this.state.CommentaryText}</p>
            <div>
              <input className='CommentaryEditInput' value={this.state.ReplyText} placeholder={"Write your reply here.."}
                onChange={(event) => this.setState({ ReplyText: event.target.value })}></input>
              <div>
                <button onClick={this.AddReplyRequest}>Save</button>
                <button onClick={() => this.setState({ ReplyState: "None", ReplyText: "" })}>Cancel</button>
              </div>
            </div>          
          </div>
        )
      case "None":
        return (
          <div className='Reply'>
            <div style={{ display: "flex" }}>
              <p className='ReplyTopText'>{this.props.Author + "    commented " + this.state.DayDistance + " days ago"}</p>
              {this.state.CanBeEdited && <p style={{ color: "blue", marginLeft: "100px" }} onClick={() => this.setState({ ReplyState: "EditCommentary" })}><u>Edit.</u></p>}
            </div>
            <p className='CommentaryText'>{this.state.CommentaryText}</p>         
          </div>
        )
      case "EditCommentary":
        if (!this.state.IsBeingDeleted) {
          return (
            <div className='Reply'>
              <textarea className='CommentaryEditMode' value={this.state.CommentaryText} onChange={(event) => this.setState({ CommentaryText: event.target.value })}></textarea>
              <div>
                <button onClick={this.EditCommentaryRequest}>Save</button>
                <button onClick={this.CancelCommentEditing}>Cancel</button>
                {this.state.IsBeingDeleted ? <button onClick={() => this.ChangeDeletingState(false)}>Cancel delete</button> : <button onClick={() => this.ChangeDeletingState(true)}>Delete</button>}

              </div>             
            </div>
          )
        }
        else {
          return (
            <div className='Reply'>
              <div>
                {this.state.IsBeingDeleted ? <button onClick={() => this.ChangeDeletingState(false)}>Cancel delete</button> : <button onClick={() => this.ChangeDeletingState(true)}>Delete</button>}
              </div>
            </div>
          )
        }

    }
  }
}
