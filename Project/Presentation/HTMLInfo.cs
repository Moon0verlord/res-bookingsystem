﻿static class HTMLInfo
{
    // This is the HTML cod for the email that is sent to the user when they make a reservation.
    public static string GetHTML(string date, string code, string timeslot)
    {
        string htmlBody = $@"
<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head>
<!--[if gte mso 15]>
<xml>
<o:OfficeDocumentSettings>
<o:AllowPNG/>
<o:PixelsPerInch>96</o:PixelsPerInch>
</o:OfficeDocumentSettings>
</xml>
<![endif]-->
<meta charset='UTF-8'>
<meta http-equiv='X-UA-Compatible' content='IE=edge'>
<meta name='viewport' content='width=device-width, initial-scale=1'>
<title>*|MC:MC_SUBJECT|*</title>
<link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Gilda+Display:400,400i,700,700i,900,900i|Lora:400,400i,700,700i,900,900i|Merriweather+Sans:400,400i,700,700i,900,900i|Source+Sans+Pro:400,400i,700,700i,900,900i|Source+Code+Pro:400,400i,700,700i,900,900i'><style>          img{{-ms-interpolation-mode:bicubic;}} 
          table, td{{mso-table-lspace:0pt; mso-table-rspace:0pt;}} 
          .mceStandardButton, .mceStandardButton td, .mceStandardButton td a{{mso-hide:all !important;}} 
          p, a, li, td, blockquote{{mso-line-height-rule:exactly;}} 
          p, a, li, td, body, table, blockquote{{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%;}} 
          @media only screen and (max-width: 480px){{
            body, table, td, p, a, li, blockquote{{-webkit-text-size-adjust:none !important;}} 
          }}
          .mcnPreviewText{{display: none !important;}} 
          .bodyCell{{margin:0 auto; padding:0; width:100%;}}
          .ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font{{line-height:100%;}} 
          .ReadMsgBody{{width:100%;}} .ExternalClass{{width:100%;}} 
          a[x-apple-data-detectors]{{color:inherit !important; text-decoration:none !important; font-size:inherit !important; font-family:inherit !important; font-weight:inherit !important; line-height:inherit !important;}} 
            body{{height:100%; margin:0; padding:0; width:100%; background: #ffffff;}}
            p{{margin:0; padding:0;}} 
            table{{border-collapse:collapse;}} 
            td, p, a{{word-break:break-word;}} 
            h1, h2, h3, h4, h5, h6{{display:block; margin:0; padding:0;}} 
            img, a img{{border:0; height:auto; outline:none; text-decoration:none;}} 
            a[href^='tel'], a[href^='sms']{{color:inherit; cursor:default; text-decoration:none;}} 
            li p {{margin: 0 !important;}}
            .ProseMirror a {{
                pointer-events: none;
            }}
            @media only screen and (max-width: 480px){{
                body{{width:100% !important; min-width:100% !important; }} 
                body.mobile-native {{
                    -webkit-user-select: none; user-select: none; transition: transform 0.2s ease-in; transform-origin: top center;
                }}
                body.mobile-native.selection-allowed a, body.mobile-native.selection-allowed .ProseMirror {{
                    user-select: auto;
                    -webkit-user-select: auto;
                }}
                colgroup{{display: none;}}
                img{{height: auto !important;}}
                .mceWidthContainer{{max-width: 660px !important;}}
                .mceColumn{{display: block !important; width: 100% !important;}}
                .mceColumn-forceSpan{{display: table-cell !important; width: auto !important;}}
                .mceBlockContainer{{padding-right:16px !important; padding-left:16px !important;}} 
                .mceSpacing-24{{padding-right:16px !important; padding-left:16px !important;}}
                .mceFooterSection .mceText, .mceFooterSection .mceText p{{font-size: 16px !important; line-height: 140% !important;}}
                .mceText, .mceText p{{font-size: 16px !important; line-height: 140% !important;}}
                h1{{font-size: 30px !important; line-height: 120% !important;}}
                h2{{font-size: 26px !important; line-height: 120% !important;}}
                h3{{font-size: 20px !important; line-height: 125% !important;}}
                h4{{font-size: 18px !important; line-height: 125% !important;}}
                .ProseMirror {{
                    -webkit-user-modify: read-write-plaintext-only;
                    user-modify: read-write-plaintext-only;
                }}
            }}
            @media only screen and (max-width: 640px){{
                .mceClusterLayout td{{padding: 4px !important;}} 
            }}
            div[contenteditable='true'] {{outline: 0;}}
            .ProseMirror .empty-node, .ProseMirror:empty {{position: relative;}}
            .ProseMirror .empty-node::before, .ProseMirror:empty::before {{
                position: absolute;
                left: 0;
                right: 0;
                color: rgba(0,0,0,0.2);
                cursor: text;
            }}
            .ProseMirror .empty-node:hover::before, .ProseMirror:empty:hover::before {{
                color: rgba(0,0,0,0.3);
            }}
            .ProseMirror h1.empty-node::before {{
                content: 'Heading';
            }}
            .ProseMirror p.empty-node:only-child::before, .ProseMirror:empty::before {{
                content: 'Start typing...';
            }}
            a .ProseMirror p.empty-node::before, a .ProseMirror:empty::before {{
                content: '';
            }}
            .mceText, .ProseMirror {{
                white-space: pre-wrap;
            }}
body, #bodyTable {{ background-color: rgb(71, 101, 132); }}.mceText, .mceLabel {{ font-family: 'Source Sans Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif; }}.mceText, .mceLabel {{ color: rgb(77, 77, 77); }}.mceText h1 {{ margin-bottom: 0px; }}.mceText h2 {{ margin-bottom: 0px; }}.mceText h3 {{ margin-bottom: 0px; }}.mceText h4 {{ margin-bottom: 0px; }}.mceText p {{ margin-bottom: 0px; }}.mceText label {{ margin-bottom: 0px; }}.mceText input {{ margin-bottom: 0px; }}.mceSpacing-24 .mceInput + .mceErrorMessage {{ margin-top: -12px; }}.mceText h1 {{ margin-bottom: 0px; }}.mceText h2 {{ margin-bottom: 0px; }}.mceText h3 {{ margin-bottom: 0px; }}.mceText h4 {{ margin-bottom: 0px; }}.mceText p {{ margin-bottom: 0px; }}.mceText label {{ margin-bottom: 0px; }}.mceText input {{ margin-bottom: 0px; }}.mceSpacing-12 .mceInput + .mceErrorMessage {{ margin-top: -6px; }}.mceInput {{ background-color: transparent; border: 2px solid rgb(208, 208, 208); width: 60%; color: rgb(77, 77, 77); display: block; }}.mceInput[type='radio'], .mceInput[type='checkbox'] {{ float: left; margin-right: 12px; display: inline; width: auto !important; }}.mceLabel > .mceInput {{ margin-bottom: 0px; margin-top: 2px; }}.mceLabel {{ display: block; }}.mceText p {{ color: rgb(77, 77, 77); font-family: 'Source Sans Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: normal; line-height: 1.5; text-align: left; letter-spacing: 0px; direction: ltr; }}.mceText h1 {{ color: rgb(71, 101, 132); font-family: Lora, Georgia, 'Times New Roman', serif; font-size: 60px; font-weight: bold; line-height: 1.5; text-align: left; letter-spacing: 0px; direction: ltr; }}.mceText h2 {{ color: rgb(48, 76, 104); font-family: Lora, Georgia, 'Times New Roman', serif; font-size: 32px; font-weight: bold; line-height: 1.5; text-align: left; letter-spacing: 0px; direction: ltr; }}.mceText h3 {{ color: rgb(71, 101, 132); font-family: 'Source Code Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold; line-height: 1.5; text-align: left; letter-spacing: 0px; direction: ltr; }}.mceText h4 {{ color: rgb(220, 17, 29); font-family: 'Source Code Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 20px; font-weight: bold; line-height: 1.5; text-align: left; letter-spacing: 0px; direction: ltr; }}
@media only screen and (max-width: 480px) {{
            .mceText p {{ font-size: 16px !important; line-height: 1.5 !important; }}
          }}
@media only screen and (max-width: 480px) {{
            .mceText h1 {{ font-size: 44px !important; line-height: 1.5 !important; }}
          }}
@media only screen and (max-width: 480px) {{
            .mceText h2 {{ font-size: 28px !important; line-height: 1.5 !important; }}
          }}
@media only screen and (max-width: 480px) {{
            .mceText h3 {{ font-size: 14px !important; line-height: 1.5 !important; }}
          }}
@media only screen and (max-width: 480px) {{
            .mceText h4 {{ font-size: 24px !important; line-height: 1.5 !important; }}
          }}
@media only screen and (max-width: 480px) {{
            .mceBlockContainer {{ padding-left: 16px !important; padding-right: 16px !important; }}
          }}
#dataBlockId-38 p, #dataBlockId-38 h1, #dataBlockId-38 h2, #dataBlockId-38 h3, #dataBlockId-38 h4, #dataBlockId-38 ul {{ text-align: end; }}#dataBlockId-38 p, #dataBlockId-38 h1, #dataBlockId-38 h2, #dataBlockId-38 h3, #dataBlockId-38 h4, #dataBlockId-38 ul {{ text-align: center; }}</style></head>
<body>
<!---->
<center>
<table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style='background-color: rgb(71, 101, 132);'>
<tbody><tr>
<td class='bodyCell' align='center' valign='top'>
<table id='root' border='0' cellpadding='0' cellspacing='0' width='100%'><tbody data-block-id='42' class='mceWrapper'><tr><td align='center' valign='top' class='mceWrapperOuter'><!--[if (gte mso 9)|(IE)]><table align='center' border='0' cellspacing='0' cellpadding='0' width='660' style='width:660px;'><tr><td><![endif]--><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width:660px' role='presentation'><tbody><tr><td style='background-color:#ffffff;background-position:center;background-repeat:no-repeat;background-size:cover' class='mceWrapperInner' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='41'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:0;padding-bottom:0' class='mceColumn' data-block-id='-16' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='background-color:#476584;padding-top:0;padding-bottom:0;padding-right:0;padding-left:0' class='mceBlockContainer' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:#476584' role='presentation' data-block-id='1'><tbody><tr><td style='min-width:100%;border-top:30px solid transparent' valign='top'></td></tr></tbody></table></td></tr><tr><td style='padding-top:5px;padding-bottom:0;padding-right:0;padding-left:0' class='mceLayoutContainer' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='7' id='section_97e40caaa7de994cae374fe8a1fa7410' class='mceLayout'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-14' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td align='center' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='-5'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-20' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='6'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover;padding-top:0px;padding-bottom:0px' valign='top'><table border='0' cellpadding='0' cellspacing='24' width='100%' style='table-layout:fixed' role='presentation'><colgroup><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'></colgroup><tbody><tr><td style='padding-top:0;padding-bottom:0' class='mceColumn' data-block-id='3' valign='top' colspan='8' width='66.66666666666666%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:12px;padding-bottom:5px;padding-right:24px;padding-left:24px' class='mceBlockContainer' valign='top'><div data-block-id='2' class='mceText' id='dataBlockId-2' style='width:100%'><h1 style='text-align: center;' class='last-child'><em><span style='font-size: 40px'><span style='font-family: Georgia, Times, 'Times New Roman', serif'>DE WITTE HAVEN</span></span></em></h1></div></td></tr></tbody></table></td><td style='padding-top:0;padding-bottom:0;margin-bottom:24px' class='mceColumn' data-block-id='5' valign='top' colspan='4' width='33.33333333333333%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:12px;padding-bottom:12px;padding-right:0;padding-left:0' class='mceBlockContainer' align='center' valign='top'><img data-block-id='4' width='179' style='width:179px;height:auto;max-width:100%;display:block' alt='' src='https://dim.mcusercontent.com/https/cdn-images.mailchimp.com%2Ftemplate_images%2Femail%2F02e89d02fa76f31fd037d1a51%2Fimages%2F1466a6bb-23c9-da04-a279-655190edbbd4.png?w=179&amp;dpr=2' role='presentation' class='imageDropZone'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td style='padding-top:12px;padding-bottom:12px;padding-right:0;padding-left:0' class='mceLayoutContainer' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='70' id='section_e952643c2f4153aea45d948aa05dbbeb' class='mceLayout'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-18' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td align='center' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='-11'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-23' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='75'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover;padding-top:0px;padding-bottom:0px' valign='top'><table border='0' cellpadding='0' cellspacing='24' width='100%' style='table-layout:fixed' role='presentation'><colgroup><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'></colgroup><tbody><tr><td style='padding-top:0;padding-bottom:0' class='mceColumn' data-block-id='72' valign='top' colspan='6' width='50%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:12px;padding-bottom:12px;padding-right:24px;padding-left:24px' class='mceBlockContainer' valign='top'><div data-block-id='76' class='mceText' id='dataBlockId-76' style='width:100%'><h3 style='text-align: left;' class='last-child'><span style='font-size: 11px'><span style='font-family: 'Merriweather Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif'>EEN CONFIRMATIE EMAIL VAN UW RESERVERING</span></span></h3></div></td></tr></tbody></table></td><td style='padding-top:0;padding-bottom:0;margin-bottom:24px' class='mceColumn' data-block-id='74' valign='top' colspan='6' width='50%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='background-color:transparent;padding-top:20px;padding-bottom:20px;padding-right:24px;padding-left:0' class='mceBlockContainer' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:transparent' role='presentation' data-block-id='77'><tbody><tr><td style='min-width:100%;border-top:3px solid #476584' valign='top'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td style='padding-top:25px;padding-bottom:12px;padding-right:0;padding-left:0' class='mceLayoutContainer' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='22' id='section_3dbe9fe5e8e74da39e759f6216e7c606' class='mceLayout'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-15' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td align='center' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='-7'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-21' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='21'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover;padding-top:0px;padding-bottom:0px' valign='top'><table border='0' cellpadding='0' cellspacing='24' width='100%' style='table-layout:fixed' role='presentation'><colgroup><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'><col span='1' width='8.333333333333332%'></colgroup><tbody><tr><td style='padding-top:0;padding-bottom:0' class='mceColumn' data-block-id='16' valign='top' colspan='6' width='50%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:10px;padding-bottom:5px;padding-right:0;padding-left:0' class='mceBlockContainer' align='center' valign='top'><img data-block-id='14' width='294' style='width:294px;height:auto;max-width:100%;display:block' alt='' src='https://dim.mcusercontent.com/cs/eaf5a3d1b6e3579027898dbbc/images/4e9667f9-4e78-e573-15f6-2db0b9c9a69e.jpg?w=294&amp;dpr=2' role='presentation' class='imageDropZone'></td></tr><tr><td style='padding-top:2px;padding-bottom:2px;padding-right:20px;padding-left:18px' class='mceBlockContainer' valign='top'><div data-block-id='15' class='mceText' id='dataBlockId-15' style='width:100%'><p class='last-child'><em><span style='font-size: 11px'><span style='font-family: 'Lora', Georgia, 'Times New Roman', serif'>Foto van Frank de Roo </span></span></em>©</p></div></td></tr><tr><td style='padding-top:17px;padding-bottom:17px;padding-right:17px;padding-left:17px' class='mceBlockContainer' valign='top'><div data-block-id='46' class='mceText' id='dataBlockId-46' style='width:100%'><h4 style='line-height: 1;'><em><span style='font-size: 10px'><span style='font-family: 'Source Sans Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif'>Let op! Annulering van een reservering is niet mogelijk in ons programma als de reservering zich binnen 24 uur bevindt, dan moet u contact opnemen met het restaurant en &nbsp;zien wat mogelijk is.</span></span></em></h4><p class='last-child'><br></p></div></td></tr></tbody></table></td><td style='padding-top:0;padding-bottom:0;margin-bottom:24px' class='mceColumn' data-block-id='20' valign='top' colspan='6' width='50%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:5px;padding-bottom:10px;padding-right:15px;padding-left:10px' class='mceBlockContainer' valign='top'><div data-block-id='17' class='mceText' id='dataBlockId-17' style='width:100%'><h2 style='text-align: center;' class='last-child'><span style='font-size: 34px'><span style='font-family: 'Gilda Display', serif'>Bedankt voor uw reservatie!</span></span></h2></div></td></tr><tr><td style='padding-top:10px;padding-bottom:10px;padding-right:15px;padding-left:6px' class='mceBlockContainer' valign='top'><div data-block-id='19' class='mceText' id='dataBlockId-19' style='width:100%'><p style='line-height: 1.25;'>Deze e-mail bevat uw reservatiecode. Met deze reservatiecode kunt u uw reservatie inzien in ons programma, en heeft u ook toegang tot annulering van de reservatie als u dit zou willen.</p><p style='line-height: 1.25;'><br></p><p style='line-height: 1.25;'>Als u uw reservatie wilt inzien zonder een account, ga dan naar ‘Reservaties bekijken’ in het hoofdmenu, en typ uw volledige reservatiecode daar in.</p><p style='line-height: 1.25;'><br></p><p style='line-height: 1.25;' class='last-child'>Als u een account heeft, kunt u gelijk al uw reservaties in een lijst bekijken, dan heeft u geen reservatiecode nodig om het in te zien of te annuleren. </p></div></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td style='background-color:transparent;padding-top:0;padding-bottom:20px;padding-right:24px;padding-left:24px' class='mceBlockContainer' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:transparent' role='presentation' data-block-id='85'><tbody><tr><td style='min-width:100%;border-top:2px solid #476584' valign='top'></td></tr></tbody></table></td></tr><tr><td style='padding-top:12px;padding-bottom:12px;padding-right:0;padding-left:0' class='mceLayoutContainer' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='81' id='section_1931b8ff8da243f59839a4fa2d4cbbd3' class='mceLayout'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-19' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td align='center' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='-13'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-24' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='84'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='24' width='100%' role='presentation'><tbody><tr><td style='padding-top:0;padding-bottom:0;margin-bottom:24px' class='mceColumn' data-block-id='83' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:12px;padding-bottom:12px;padding-right:24px;padding-left:24px' class='mceBlockContainer' valign='top'><div data-block-id='86' class='mceText' id='dataBlockId-86' style='width:100%'><h2 style='text-align: center;'><span style='font-size: 24px'><span style='font-family: 'Gilda Display', serif'>Informatie over uw reservatie</span></span></h2><p style='text-align: left;'><br></p><p style='text-align: left;'><br></p><p style='text-align: left;'><span style='font-size: 18px'>Datum:</span><strong><span style='font-size: 18px'> {date}</span></strong></p><p style='text-align: left;' class='last-child'><span style='font-size: 18px'>Tijdslot: </span><strong><span style='font-size: 18px'>{timeslot}</span></strong></p></div></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td style='background-color:transparent;padding-top:20px;padding-bottom:20px;padding-right:24px;padding-left:24px' class='mceBlockContainer' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:transparent' role='presentation' data-block-id='56'><tbody><tr><td style='min-width:100%;border-top:2px solid #476584' valign='top'></td></tr></tbody></table></td></tr><tr><td style='padding-top:12px;padding-bottom:12px;padding-right:0;padding-left:0' class='mceLayoutContainer' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='50' id='section_32e65f1ba40971f65d2117503b9fbca5' class='mceLayout'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-17' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td align='center' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='-9'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td class='mceColumn' data-block-id='-22' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='53'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover' valign='top'><table border='0' cellpadding='0' cellspacing='24' width='100%' role='presentation'><tbody><tr><td style='padding-top:0;padding-bottom:0;margin-bottom:24px' class='mceColumn' data-block-id='52' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:0;padding-bottom:0;padding-right:0;padding-left:0' class='mceBlockContainer' valign='top'><div data-block-id='54' class='mceText' id='dataBlockId-54' style='width:100%'><h2 style='text-align: center;' class='last-child'><span style='font-size: 24px'><span style='font-weight:normal;'>Uw reservatie code:</span></span></h2></div></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td style='padding-top:0;padding-bottom:12px;padding-right:24px;padding-left:24px' class='mceBlockContainer' valign='top'><div data-block-id='55' class='mceText' id='dataBlockId-55' style='width:100%'><h1 style='padding-top: 0; padding-bottom: 0; font-weight: 800!important; vertical-align: baseline; line-height: 43.2px; margin: 0; text-align: center;' class='last-child'><span style='font-size: 36px'>{code}</span></h1></div></td></tr><tr><td style='background-color:transparent;padding-top:20px;padding-bottom:20px;padding-right:24px;padding-left:24px' class='mceBlockContainer' valign='top'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:transparent' role='presentation' data-block-id='87'><tbody><tr><td style='min-width:100%;border-top:2px solid #476584' valign='top'></td></tr></tbody></table></td></tr><tr><td style='background-color:#d0d0d0;padding-top:12px;padding-bottom:12px;padding-right:12px;padding-left:12px' class='mceLayoutContainer' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='40' id='section_499e550c2f8e6496c87d8ece56ac651d' class='mceFooterSection'><tbody><tr class='mceRow'><td style='background-color:#d0d0d0;background-position:center;background-repeat:no-repeat;background-size:cover;padding-top:0px;padding-bottom:0px' valign='top'><table border='0' cellpadding='0' cellspacing='12' width='100%' role='presentation'><tbody><tr><td style='padding-top:0;padding-bottom:0;margin-bottom:12px' class='mceColumn' data-block-id='-3' valign='top' colspan='12' width='100%'><table border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation'><tbody><tr><td style='padding-top:12px;padding-bottom:12px;padding-right:16px;padding-left:16px' class='mceBlockContainer' align='right' valign='top'><div data-block-id='38' class='mceText' id='dataBlockId-38' style='display:inline-block;width:100%'><h2><em><span style='font-size: 24px'>DE WITTE HAVEN</span></em></h2><p><br></p><p class='last-child'>Wijnhaven 107<br>3011 WN / Rotterdam<br>+31-0612345678<br>restaurant1234567891011@gmail.com</p></div></td></tr><tr><td class='mceLayoutContainer' align='right' valign='top'><table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' role='presentation' data-block-id='-2'><tbody><tr class='mceRow'><td style='background-position:center;background-repeat:no-repeat;background-size:cover;padding-top:0px;padding-bottom:0px' valign='top'><table border='0' cellpadding='0' cellspacing='24' width='100%' role='presentation'><tbody></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></tbody></table>
</td>
</tr>
</tbody></table>
</center>
<script type='text/javascript' src='/cGGM_KRbwRlvdrk04A4p/E1kEJzNcQ0/WChw/U1VO/H1kRA0U'></script></body></html>
";
        return htmlBody;
    }
}





