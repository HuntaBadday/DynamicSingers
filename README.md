# DynamicSingers
Dynamic singers is a mod that adds singers whose note can be changed on the fly using a binary input.

## Mono Singer
The mono singer accepts a 7 bit input (Least significant bit on the right), and an enable signal. The 7 bit input corresponds to the note to be played, and the enable signal plays that note.

## Poly Singer
The poly singer is similar to the monophonic version, except that every one of the 128 available notes can be turned on or off individually. \
The 7 bit input selects the note to change, the first of the two pins on the right is the state the note should be, and the second writes that state to the singer. The pin on the side resets and turns off all notes.