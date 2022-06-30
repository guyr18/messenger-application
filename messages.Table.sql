CREATE TABLE [messages].[Table] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [content]      VARCHAR (500) NOT NULL,
    [sender_id]    INT           NOT NULL,
    [receiver_id]  INT           NOT NULL,
    [time_created] TIME (0)      DEFAULT ('00:00:00') NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [composite_index_messages]
    ON [messages].[Table]([content] ASC, [sender_id] ASC, [receiver_id] ASC);

