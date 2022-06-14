class Notes extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null, isLoaded: false, backend: "local", newNote: { "Id": 0, "NoteText": "" }, notes: props.notes || []
        };
    }

    componentDidMount() {
        fetch("/api/note")
            .then(res => {
                //throw "hello world";
                return res.json();
            })
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        notes: result
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            )
    }

    saveNote = (note) => {
        fetch('/api/note', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(note)
        })
            .then(res => res.json())
            .then(
                (result) => {
                    var newNotes = [...this.state.notes, result];
                    this.setState({ newNote: { "Id": 0, "NoteText": "" }, notes: newNotes });
                });
    }

    deleteNote = (note) => {
        fetch(`/api/note/${note.Id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        })
    }

    handleNewNoteKeyDown = (event) => {
        // enter key
        if (event.keyCode !== 13) {
            return;
        }

        event.preventDefault();
        this.handleSaveNote();
    }

    handleSaveNote = () => {
        var newNoteText = this.state.newNote.NoteText;
        var error = this.validateNote(newNoteText);
        if (error) {
            this.setState({ error });
        }
        else {
            var newNote = { "Id": 0, "NoteText": newNoteText };
            this.saveNote(newNote);
        }
    }

    validateNote = (newNoteText) => {
        if (!newNoteText || newNoteText == "") {
            return "no note text!";
        }
        if (newNoteText.trim() == "") {
            return "must enter text!";
        }
    }

    backendChange = (event) =>
        this.setState({ backend: event.target.value });

    handleNoteDelete = (note) => {

        this.deleteNote(note);

        var notes = this.state.notes.filter(item => item !== note);
        this.setState({
            notes
        });

        this.state.newNote = { "Id": 0, "NoteText": "" };
    };

    handleNoteEdit = (note) => {
        var notes = this.state.notes.filter(item => item !== note);
        this.setState({
            notes, newNote: note
        });
    }

    handleNewNoteChange = (event) =>
        this.setState({ newNote: { "Id": this.state.newNote.Id, "NoteText": event.target.value }, "error": "" });

    render() {
        return (
            <div className="col-md-12">
                <div className="row">
                    <div className="col-md-3">
                        <input type="text" value={this.state.newNote.NoteText} onChange={this.handleNewNoteChange} onKeyDown={this.handleNewNoteKeyDown} ></input>
                    </div>
                    <div className="col-md-3">
                        <button className="btn btn-success btn-sm" onClick={_ => this.handleSaveNote()} >Save</ button>
                    </div>
                </div>

                <div className="row text-danger">
                    {this.state.error}
                </div>

                <div>
                    {this.state.notes.map((note, index) =>
                        <div key={index} className="row">
                            <div className="col-md-3">
                                <a href={"/api/note/" + note.Id}>
                                    {note.NoteText}
                                </a>
                            </div>
                            <div className="col-md-3">
                                <button className="btn btn-primary btn-sm" onClick={_ => this.handleNoteEdit(note)} >Edit</ button>
                                <button className="btn btn-danger btn-sm" onClick={_ => this.handleNoteDelete(note)} >Delete</ button>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        )
    }
}

const root = ReactDOM.createRoot(document.getElementById('app'));
root.render(<Notes />);