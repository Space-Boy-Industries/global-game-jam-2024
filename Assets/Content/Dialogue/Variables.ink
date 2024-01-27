VAR IsTableClean = false


-> START

=== START ===
{ IsTableClean:
    WIFE: Cleaned table. Thank :)
        -> CLEANED_TABLE
- else:
    WIFE: Please clean table
    + Okay # close
        -> START
}

=== CLEANED_TABLE ===
Nice job cleaning table
    * End
        -> END
