import React, { Component } from 'react'
import CommentComponent from './CommentComponent';
import "./ComponentsCSS/CommentSectionComponent.css"

export default class CommentSectionComponent extends Component {
  constructor(props) {
    super(props)
    this.GetCommentsFromApi = this.GetCommentsFromApi.bind(this);
    this.AddCommentaryRequest = this.AddCommentaryRequest.bind(this);
    this.EditCanceled = this.EditCanceled.bind(this);

    this.state = {
      Id: this.props.Id,
      Comments: [],
      IsAddSectionShowed: false,
      NewCommentaryText: ""
    }
  }

  async componentDidMount() {
    var comments = await this.GetCommentsFromApi(this.state.Id);
    this.setState({ Comments: comments });
  }

  AddCommentaryRequest() {
    fetch('http://localhost:21409/api/comments/addcomment?postId=' + this.state.Id + '&value=' + this.state.NewCommentaryText
      , {
        method: 'POST',
        credentials : 'include',
        headers: {
          'Accept': 'application/json'
        }
      })
    this.setState({ IsAddSectionShowed: false, NewCommentaryText: "" });
    window.location.reload(false);
  }

  EditCanceled() {
    this.setState({ IsAddSectionShowed: false, NewCommentaryText: "" });
  }

  async GetCommentsFromApi(id) {
    const response = await fetch("http://localhost:21409/api/comments/getcomments?parentPostId=" + id);
    const jsonResult = await response.json()
    return jsonResult;
  }

  render() {
    switch (this.state.IsAddSectionShowed) {
      case true:
        return (
          <div>
            <div>
              <textarea className='CommentaryEditInput' value={this.state.NewCommentaryText} placeholder={"Write your commentary here.."}
                onChange={(event) => this.setState({ NewCommentaryText: event.target.value })}></textarea>
              <div>
                <button onClick={this.AddCommentaryRequest}>Save</button>
                <button onClick={this.EditCanceled}>Cancel</button>
              </div>
            </div>

            <div className='CommentarySection'>
              {this.state.Comments.map((comment) => {
                return (
                  <CommentComponent Text={comment.value} Author={comment.authorName} Date={comment.createdAt} Id={comment.id} key={comment.id} Replys={comment.replyes}></CommentComponent>
                )
              })}
            </div>
          </div>
        )
      case false:
        return (
          <div>
            <button className='AddCommentaryButton' onClick={() => this.setState({ IsAddSectionShowed: true })}>Add commentary</button>
            <div className='CommentarySection'>
              {this.state.Comments.map((comment) => {
                return (
                  <CommentComponent ParentId={this.state.Id} Text={comment.value} Author={comment.authorName} Date={comment.createdAt} Id={comment.id} key={comment.id} Replys={comment.replyes}></CommentComponent>
                )
              })}
            </div>
          </div>
        )
    }

  }
}
