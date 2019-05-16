



$(function () {
    /*
    window.onerror = function () {
        console.clear();
    }; */

    const editNoteForm = `
        <div class="input">
                <input class="input__field" type="text" name="NoteHeader">
                <span class="input__highlight"></span>
                <span class="input__bar"></span>
                <label class="input__label">Заголовок</label>
            </div>

            <div class="input">
                <textarea class="input__field" rows="13" name="NoteText"></textarea>
                <span class="input__highlight"></span>
                <span class="input__bar"></span>
                <label class="input__label">Заметка</label>
            </div>

            <button type="submit" class="create-form__submit-button">Сохранить</button>
        `;

    const preloader = `<div class="preloader">
				              <div class="preloader__circle"></div>
				              <div class="preloader__circle"></div>
				              <div class="preloader__circle"></div>
				            </div>`;


    const notesPlaceholder = `
                <div class="note-view__no-notes">
                   <p class="note-view__no-notes-text">Создавайте заметки для сохранения дополнительной информации о днях</p>	    
    			</div>`;


    var notePopup = $.magnificPopup.instance;
    $('.note-card__header').magnificPopup({
        type: 'inline',
        closeOnBgClick: true,
        showCloseBtn: false,
        midClick: true, // Allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source in href.
        // Delay in milliseconds before popup is removed
        removalDelay: 300,

        // Class that is added to popup wrapper and background
        // make it unique to apply your CSS animations just to this exact popup
        mainClass: 'mfp-fade'
    });


    $('#closeNotePopup').on('click', function () {
        document.getElementById('editNoteForm').innerHTML = preloader;
        notePopup.close();
    });



    





    const noteButtons = document.querySelectorAll('.option--note');
    noteButtons.forEach(note => note.addEventListener('click', function(e) {
        noteId = parseInt(e.target.closest('.week__mark-options').dataset.identity);

        //set note id to form
        document.querySelector('#editNoteForm').setAttribute('data-noteid', noteId);

        $.magnificPopup.open({
            items: {
                src: '#editNote',
            },
            type: 'inline'
        });

        var request = $.ajax(
            {
                type: "POST", //HTTP POST Method  
                url: "Habit/GetNote", // Controller/View   
                data: { //Passing data   
                    dayId: noteId
                }
            });
        request.done(function (msg) {
            console.log(msg);
            const note = JSON.parse(msg);
            const editForm = document.getElementById('editNoteForm');
            editForm.innerHTML = editNoteForm;
            editForm.elements.NoteHeader.value = note.Headline;
            editForm.elements.NoteText.value = note.NoteText;
            
        });

        request.fail(function () {
            alert('Произошла ошибка при загрузке заметки');
        });


    }));

    // SAVE NOTE
    document.getElementById('editNoteForm').addEventListener('submit', (e) => {
        e.preventDefault();
        const editForm = document.getElementById('editNoteForm');

        // get note id from form
        const noteId = parseInt(e.target.closest('form').dataset.noteid);


        const day = document.querySelector(`.day[data-identity="${noteId}"]`);
        if (editForm.elements.NoteHeader.value != '' || editForm.elements.NoteText.value != '') {
            day.classList.remove('day--note');
            day.classList.add('day--note');
            console.log('added class');
        } else {
            day.classList.remove('day--note');
            console.log('removed class');
        }
        var request = $.ajax(
            {
                type: "POST", //HTTP POST Method  
                url: "Habit/SaveNote", // Controller/View   
                data: { //Passing data   
                    dayId: noteId,
                    header: editForm.elements.NoteHeader.value,
                    noteText: editForm.elements.NoteText.value
                }
            });

        request.done(function () {
            console.log('Getting all notes');
            GetAllNotes();
        });

        request.fail(function () {
            alert('Произошла ошибка при сохранении заметки');
        });
    });

    //GETTING ALL NOTES
    $('.owl-carousel').on('changed.owl.carousel', GetAllNotes);

    function GetAllNotes() {
        if (document.querySelector('.habit-tab--active').getAttribute("href") == '#note') {

            //CONSOLE LOG
            console.warn('GETTING ALL NOTES');

            const habitId = parseInt(document.querySelector('[data-habitid]').dataset.habitid);
            var request = $.ajax(
                {
                    type: "POST", //HTTP POST Method  
                    url: "Habit/GetAllNotes", // Controller/View   
                    data: { //Passing data   
                        habitId: habitId
                    }
                });
            request.done(function (msg) {

                const notes = JSON.parse(msg);
                console.log(notes);

                RenderNotes(notes);

            });

            request.fail(function () {
                alert('Произошла ошибка при загрузке заметок');
            });
        }
    }

    function ShortenNote(noteText) {
        if (noteText.length < 70) {
            return noteText;
        } if (noteText.length < 300) {
            return noteText.substring(0, 70 + Math.round((noteText.length - 70) * 0.3)) + '...';
        } else {
            return noteText.substring(0, 300) + '...';
        }
    }

    var $grid;
    function RenderNotes(notes) {

        if (notes.length == 0) {
            document.querySelector('.note-view').innerHTML = notesPlaceholder;
        } else {
            document.querySelector('.note-view').innerHTML = notes
                .map(note => {
                    return `
                        <div class="note-card" data-noteId="${note.DayId}">
    				            <h4 class="note-card__header">${note.Headline}</h4>
    				            <p class="note-card__note">${ShortenNote(note.NoteText)}</p>
    				            <div class="note-card__footer">
    					            <span class="note-card__date">${note.date}</span>
    					            <button class="card__more-button more-menu-button">
							            <img src="/Content/Images/more.svg" alt="" class="card__more-icon">     
						            </button>
						            <div class="note-card__more-menu card__more-menu more-menu">
							            <h4 class="more-menu__item">Удалить</h4>
						            </div>
    				            </div>
    			            </div>
                    `;
                }).join('');
        }

        


        $('.more-menu-button').on('click', function (e) {
            var menu = $(e.currentTarget).siblings('.more-menu');
            if (menu.hasClass('more-menu--open')) {
                menu.removeClass('more-menu--open');
            } else {
                menu.addClass('more-menu--open');
            }

        });

        $('.more-menu').mouseleave(function (e) {
            $(e.currentTarget).removeClass('more-menu--open');
        });

        

        $(window).scroll(function () {        
            $('.more-menu--open').removeClass('more-menu--open');
        });
        
        $grid = $('.note-view').masonry({
            itemSelector: '.note-card',
            columnWidth: '.note-card',
            percentPosition: true,
            gutter: 8,
            resize: true,
            containerStyle: null
        });
        //window.dispatchEvent(new Event('resize'));
        //$grid.masonry();

        //Open note
        openNote(notes);


        // DELETE NOTE
        $('.note-card__more-menu .more-menu__item').on('click', function (e) {
            let noteCard = $(e.currentTarget).closest('.note-card');
            let noteId = noteCard.attr('data-noteId');


            var request = $.ajax(
                {
                    type: "POST", //HTTP POST Method  
                    url: "Habit/DeleteNote", // Controller/View   
                    data: { //Passing data   
                        dayId: noteId
                    }
                });
            request.done(function (msg) {

                noteCard.hide();
                $grid.masonry('destroy');
                $grid = $('.note-view').masonry({
                    itemSelector: '.note-card',
                    columnWidth: '.note-card',
                    percentPosition: true,
                    gutter: 8,
                    resize: true,
                    containerStyle: null
                });
                //window.dispatchEvent(new Event('resize'));
                //$grid.masonry();
                console.log(document.querySelector(`.day[data-identity="${noteId}"]`));
                document.querySelector(`.day[data-identity="${noteId}"]`).classList.remove('day--note');

            });

            request.fail(function () {
                alert('Произошла ошибка при удалении заметки');
            });

            
        });
    }

    
    $(window).resize(function (e) {
        $grid.masonry();
    }); 

    // OPEN NOTE IN NOTE TAB
    function openNote(notes) {
        const noteButtons = document.querySelectorAll('.note-card__header');
        noteButtons.forEach(note => note.addEventListener('click', function (e) {
            console.log(e.target.closest('.note-card'));
            const noteId = parseInt(e.target.closest('.note-card').dataset.noteid);

            //set note id to form
            document.querySelector('#editNoteForm').setAttribute('data-noteid', noteId);


            console.log(noteId);
            const noteInfo = notes.find(n => n.DayId == noteId);

            console.log(noteInfo);
            $.magnificPopup.open({
                items: {
                    src: '#editNote',
                },
                type: 'inline'
            });

            const editForm = document.getElementById('editNoteForm');
            editForm.innerHTML = editNoteForm;
            editForm.elements.NoteHeader.value = noteInfo.Headline;
            editForm.elements.NoteText.value = noteInfo.NoteText;
        }));
    }


    

    
});
