CREATE TABLE UserAnswer (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT,
    QuizId INTEGER NOT NULL,
    QuestionId INTEGER NOT NULL,
    AnswerId INTEGER NOT NULL,
    IsCorrect INTEGER NOT NULL, -- Use 0 or 1 for boolean
    Answered TEXT NOT NULL DEFAULT (datetime('now')),

    FOREIGN KEY (QuestionId) REFERENCES Question(Id) ON DELETE CASCADE,
    FOREIGN KEY (AnswerId) REFERENCES Answer(Id) ON DELETE CASCADE
);