var SERVER_URL = 'http://api.kangfupanda.com/'
var Resource_URL = 'http://api.kangfupanda.com/site/'
var $header = `<!-- !Top-bar -->
<div id="top-bar" role="complementary" class="bit-top-bar"
    style="line-height:30px;padding-right:0px;padding-left:0px;">
    <div class="wf-wrap">
        <div class="wf-table wf-mobile-collapsed">

            <div class="wf-td">
                <div class="mini-contacts wf-float-left">
                    <ul>
                        <li></li>
                    </ul>
                </div>
            </div>
        </div><!-- .wf-table -->
    </div><!-- .wf-wrap -->
</div><!-- #top-bar -->
<!-- left, center, classical, classic-centered -->
<!-- !Header -->
<header id="header" class="logo-left-right headerPM menuPosition transparent" role="banner">
    <!-- class="overlap"; class="logo-left", class="logo-center", class="logo-classic" -->
    <div class="wf-wrap">
        <div class="wf-table">



            <div id="branding" class="wf-td bit-logo-bar" style="">
                <a class="bitem logo nomarl" style="display: table-cell;"
                    href="/"><span class="logospan"><img class="preload-me"
                            src="image/logo.png"
                            width="194" height="51" alt="一健点评" /></span></a>

                <!-- <div id="site-title" class="assistive-text"></div>
<div id="site-description" class="assistive-text"></div> -->
            </div>

            <!-- !- Navigation -->
            <nav style="0" id="navigation" class="wf-td" bitDataAction="site_menu_container"
                bitDataLocation="primary">
                <ul id="main-nav" data-st="0" data-sp="0" data-fh="0" data-mw="1280" data-lh="48"
                    class="mainmenu fancy-rollovers wf-mobile-hidden bit-menu-default underline-hover position-ab-center ab-center"
                    data-bit-menu=underline-hover data-bit-float-menu=underline-hover
                    data-sliderdown=sliderdown>
                    <!-- <li
                        class=" menu-item menu-item-type-post_type menu-item-object-page current-menu-item page_item page-item-2160 current_page_item bit-menu-post-id-2160 menu-item-2319 act first">
                        <a href="index.html"><span>网站首页</span></a></li>-->
                    <li
                        class=" menu-item menu-item-type-post_type menu-item-object-page bit-menu-post-id-9528 menu-item-has-children menu-item-9781 has-children">
                        <a href="index.html"><span>关于我们</span></a>
                    </li>
                    <li
                        class=" menu-item menu-item-type-post_type menu-item-object-page bit-menu-post-id-6622 menu-item-6623">
                        <a href="yxs.html"><span>研习社</span></a></li>
                    <li
                        class=" menu-item menu-item-type-post_type menu-item-object-page bit-menu-post-id-6628 menu-item-has-children menu-item-6629 has-children">
                        <a href="classicCase.html"><span>经典案例</span></a>
                    </li>
                    <li
                        class=" menu-item menu-item-type-post_type menu-item-object-page bit-menu-post-id-9484 menu-item-has-children menu-item-9485 has-children">
                        <a href="doctor.html"><span>合作专家</span></a>
                    </li>
                    <li
                        class=" menu-item menu-item-type-post_type menu-item-object-page bit-menu-post-id-7388 menu-item-6632">
                        <a href="newCenter.html"><span>资讯中心</span></a></li>
                    <li
                        class=" menu-item menu-item-type-post_type menu-item-object-page bit-menu-post-id-6633 menu-item-6634">
                        <a href="contact.html"><span>联系我们</span></a></li>
                </ul>

                <a data-padding='' data-top='15' data-right='8' rel="nofollow" id="mobile-menu"
                    style="display:none;" class="glyphicon glyphicon-icon-align-justify floatmenu center">
                    <span class="menu-open  phone-text">首页</span>
                    <span class="menu-close">关闭</span>
                    <span class="menu-back">返回上一级</span>
                    <span class="wf-phone-visible">&nbsp;</span>
                </a>


            </nav>

           
            </div>
        </div><!-- #branding -->
    </div><!-- .wf-wrap -->
</header><!-- #masthead -->`;

var $footer = `<footer id="footer" class="footer">
<div class="wf-wrap">
    <div class="wf-container qfe_row footer1" bitDataAction='site_widget_container'
        bitdatamarker="sidebar_2">
        <section id="text-34" style="margin-top:0;margin-bottom:0;"
            class="desktopHidden widget widget_text site_tooler">
            <div class="textwidget ckeditorInLine bitWidgetFrame" bitRlt="text" bitKey="text" wid="text-34">
                <div style="text-align: center;"><span style="font-size:12px;"><span
                            style="color: rgb(255, 255, 255);">版权所有 ©2014 &nbsp;
                            地址：上海市松江区佘山林荫大道18号</span></span></div>
            </div>
        </section>
        <section id="simplepage-2" style="margin-top:0;margin-bottom:0;"
            class="mobileHidden widget simplepage site_tooler">
            <style class='style_simplepage-2'>
                #simplepage-2 .widget-title {
                    padding: 0 0 0 10px;
                    height: 28px;
                    line-height: 28px;
                    background-color: transparent;
                    margin: 0px;
                    font-family: ;
                    font-size: px;
                    font-weight: normal;
                    font-style: normal;
                    text-decoration: none;
                    color: #ffffff;
                    border-top: 1px solid transparent;
                    border-left: 1px solid transparent;
                    border-right: 1px solid transparent;
                    border-bottom: 0px solid transparent;
                    background-image: none;
                    -webkit-border-top-left-radius: 4px;
                    -webkit-border-top-right-radius: 4px;
                    -moz-border-radius-topleft: 4px;
                    -moz-border-radius-topright: 4px;
                    border-top-left-radius: 4px;
                    border-top-right-radius: 4px;
                }

                #simplepage-2 .widget-title {
                    border-top: 0;
                    border-left: 0;
                    border-right: 0;
                }

                #simplepage-2 .bitWidgetFrame {
                    border-bottom: 0;
                    border-top: 0;
                    border-left: 0;
                    border-right: 0;
                    padding: 4px 10px 4px 10px;
                }

                #simplepage-2 {
                    -webkit-box-shadow: none;
                    box-shadow: none;
                }

                #simplepage-2 .bitWidgetFrame {
                    background-color: transparent;
                    background-image: none;
                    -webkit-border-bottom-left-radius: 4px;
                    border-bottom-left-radius: 4px;
                    -webkit-border-bottom-right-radius: 4px;
                    border-bottom-right-radius: 4px;
                }

                #simplepage-2 .bitWidgetFrame {
                    padding-left: 0px;
                    padding-right: 0px;
                }

                body #simplepage-2 .bitWidgetFrame {
                    padding-top: 0px !important;
                    padding-bottom: 0px !important;
                }
            </style>
            <div class='simplepage_container bitWidgetFrame' data-post_id='6570'>
                
                <section data-fixheight=""
                    class="qfy-row-13-5ed3aa0ec312e337733 section     no  section-text-no-shadow section-inner-no-shadow section-normal section-orgi"
                    id="bit_og2op"
                    style='margin-bottom:0;border-radius:0px;border-top:1px solid rgba(116,133,145,1);border-bottom:0px solid rgba(255,255,255,1);border-left:0px solid rgba(255,255,255,1);border-right:0px solid rgba(255,255,255,1);color:#808080;'>
                    <style class="row_class qfy_style_class">
                        @media only screen and (min-width: 992px) {
                            section.section.qfy-row-13-5ed3aa0ec312e337733 {
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 30px;
                                padding-bottom: 30px;
                                margin-top: 0;
                            }

                            section.section.qfy-row-13-5ed3aa0ec312e337733>.container {
                                max-width: 1200px;
                                margin: 0 auto;
                            }
                        }

                        @media only screen and (max-width: 992px) {
                            .bit-html section.section.qfy-row-13-5ed3aa0ec312e337733 {
                                padding-left: 15px;
                                padding-right: 15px;
                                padding-top: 30px;
                                padding-bottom: 30px;
                                margin-top: 0;
                                min-height: 0;
                            }
                        }
                    </style>
                    <div class="section-background-overlay background-overlay grid-overlay-0 "
                        style="background-color: #224d77;"></div>

                    <div class="container">
                        <div class="row qfe_row">
                            <div data-animaleinbegin="90%" data-animalename="qfyfadeInUp" data-duration=""
                                data-delay=""
                                class=" qfy-column-24-5ed3aa0ec3241389708 qfy-column-inner  vc_span_class  vc_span6  text-default small-screen-undefined notfullrow"
                                data-dw="1/2" data-fixheight="">
                                <div style=";position:relative;" class="column_inner ">
                                    <div class=" background-overlay grid-overlay-"
                                        style="background-color:transparent;width:100%;"></div>
                                    <div class="column_containter" style="z-index:3;position:relative;">
                                        <div m-padding="0px 0px 0px 0px" p-padding="0 0 0 0"
                                            css_animation_delay="0" qfyuuid="qfy_column_text_ktqsy"
                                            data-anitime='0.7'
                                            class="qfy-element qfy-text qfy-text-83561 qfe_text_column qfe_content_element  mobile_fontsize "
                                            style="position: relative;;;line-height:1.5em;;background-position:left top;background-repeat: no-repeat;;margin-top:0;margin-bottom:0;padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;border-radius:0px;">
                                            <div class="qfe_wrapper">
                                                <div style="line-height: 24px;"><span
                                                        style="font-size: 14px; color: rgb(255, 255, 255);">&nbsp;</span><span
                                                        style="font-size: 13px; color: rgb(255, 255, 255);">关于我们 &nbsp; | &nbsp; 研习社 &nbsp; |
                                                        &nbsp; 经典案例 &nbsp; | &nbsp; 合作专家 &nbsp; | &nbsp;
                                                        资讯中心 &nbsp; | &nbsp; 联系我们</span></div>
                                                <div style="line-height: 24px;"><br></div>
                                                <div style="line-height: 26px; letter-spacing: inherit;">
                                                    <span
                                                        style="font-size: 12px; color: rgb(255, 255, 255);">电话：400 188 2148
                                                        &nbsp; &nbsp; &nbsp;
                                                    </span>
                                                </div>
                                                <div style="line-height: 26px; letter-spacing: inherit;">
                                                    <span
                                                        style="font-size: 12px; color: rgb(255, 255, 255);">一健点评
                                                        版权所有 ©2014-2020<span
                                                            style="color: rgb(255, 255, 255);">&nbsp; &nbsp;
                                                            &nbsp; &nbsp;</span>备案号：浙ICP备19052964号-1<span
                                                            style="color: rgb(255, 255, 255);">&nbsp; &nbsp;
                                                            &nbsp; &nbsp;</span></span><span
                                                        style="font-size: 13px; color: rgb(255, 255, 255);"></span>
                                                </div>
                                                <div style="line-height: 24px;"><span
                                                        style="font-size: 13px; color: rgb(255, 255, 255);">
                                                    </span></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <style class="column_class qfy_style_class">
                                @media only screen and (min-width: 992px) {
                                    .qfy-column-24-5ed3aa0ec3241389708>.column_inner {
                                        padding-left: 0;
                                        padding-right: 0;
                                        padding-top: 0;
                                        padding-bottom: 0;
                                    }

                                    .qfe_row .vc_span_class.qfy-column-24-5ed3aa0ec3241389708 {
                                        width: 82.06071428571428%;
                                    }

                                    ;
                                }

                                @media only screen and (max-width: 992px) {
                                    .qfy-column-24-5ed3aa0ec3241389708>.column_inner {
                                        margin: 0 auto 0 !important;
                                        padding-left: 0;
                                        padding-right: 0;
                                        padding-top: ;
                                        padding-bottom: ;
                                    }

                                    .display_entire .qfe_row .vc_span_class.qfy-column-24-5ed3aa0ec3241389708 {
                                        width: 82.06071428571428%;
                                    }

                                    .qfy-column-24-5ed3aa0ec3241389708>.column_inner>.background-overlay,
                                    .qfy-column-24-5ed3aa0ec3241389708>.column_inner>.background-media {
                                        width: 100% !important;
                                        left: 0 !important;
                                        right: auto !important;
                                    }
                                }
                            </style>
                            <div data-animaleinbegin="bottom-in-view" data-animalename="qfyfadeInUp"
                                data-duration="" data-delay=""
                                class=" qfy-column-25-5ed3aa0ec33a3764837 qfy-column-inner  vc_span_class  vc_span6  text-default small-screen-default notfullrow"
                                data-dw="1/2" data-fixheight="">
                                <div style=";position:relative;" class="column_inner ">
                                    <div class=" background-overlay grid-overlay-"
                                        style="background-color:transparent;width:100%;"></div>
                                    <div class="column_containter" style="z-index:3;position:relative;">
                                        <div id="vc_img_5ed3aa0ec38d9474"
                                            style="padding:0px;margin:0px;clear:both;position:relative;margin-top:0;margin-bottom:0;padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;background-position:0 0;background-repeat: no-repeat;"
                                            m-padding="0px 0px 0px 0px" p-padding="0 0 0 0"
                                            css_animation_delay="0" qfyuuid="qfy_single_image_hpy7i"
                                            data-anitime='0.7' class="qfy-element bitImageControlDiv ">
                                            <style>
                                                @media only screen and (max-width: 768px) {
                                                    .single_image_text-5ed3aa0ec38ec423 .head {
                                                        font-size: 16px !important;
                                                    }

                                                    .single_image_text-5ed3aa0ec38ec423 .content {
                                                        font-size: 16px !important;
                                                    }
                                                }
                                            </style><a class="bitImageAhover  ">
                                                <div
                                                    class="bitImageParentDiv qfe_single_image qfe_content_element vc_align_right">
                                                    <div class="qfe_wrapper"><span></span><img
                                                            class="front_image   ag_image"
                                                            src="image/qrcode1.jpg"
                                                            alt="1533725425" description=""
                                                            data-attach-id="8760" data-title="1533725425"
                                                            title="" src-img="" style='' /></div>
                                                </div>
                                        </div> </a>
                                        <div m-padding="8px 0px 0px 0px" p-padding="8px 0 0 0"
                                            css_animation_delay="0" qfyuuid="qfy_column_text_hiuou"
                                            data-anitime='0.7'
                                            class="qfy-element qfy-text qfy-text-9687 qfe_text_column qfe_content_element  "
                                            style="position: relative;;;line-height:1.5em;;background-position:left top;background-repeat: no-repeat;;margin-top:0;margin-bottom:0;padding-top:8px;padding-bottom:0;padding-right:0;padding-left:0;border-radius:0px;">
                                            <div class="qfe_wrapper">
                                                <div style="text-align: right;"><span
                                                        style="font-size: 13px; color: rgb(255, 255, 255);">关注微信公众号</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <style class="column_class qfy_style_class">
                                @media only screen and (min-width: 992px) {
                                    .qfy-column-25-5ed3aa0ec33a3764837>.column_inner {
                                        padding-left: 0;
                                        padding-right: 0;
                                        padding-top: 0;
                                        padding-bottom: 0;
                                    }

                                    .qfe_row .vc_span_class.qfy-column-25-5ed3aa0ec33a3764837 {
                                        width: 9.9%;
                                    }

                                    ;
                                }

                                @media only screen and (max-width: 992px) {
                                    .qfy-column-25-5ed3aa0ec33a3764837>.column_inner {
                                        margin: 0 auto 0 !important;
                                        padding-left: 0;
                                        padding-right: 0;
                                        padding-top: ;
                                        padding-bottom: ;
                                    }

                                    .display_entire .qfe_row .vc_span_class.qfy-column-25-5ed3aa0ec33a3764837 {
                                        width: 17.9%;
                                    }

                                    .qfy-column-25-5ed3aa0ec33a3764837>.column_inner>.background-overlay,
                                    .qfy-column-25-5ed3aa0ec33a3764837>.column_inner>.background-media {
                                        width: 100% !important;
                                        left: 0 !important;
                                        right: auto !important;
                                    }
                                }
                            </style>
                            <div class="wf-mobile-hidden qfy-clumn-clear" style="clear:both;"></div>
                        </div>
                    </div>

                </section>
            </div>
        </section>
    </div>
</div><!-- .wf-wrap -->
<!--  ************begin************* -->
<style type="text/css" id="static-stylesheet-footer">
    #footer.footer .footer1 .widget {
        width: 99%;
    }

    #footer.footer .footer1 .widget.simplepage {
        width: 100%;
    }

    .bit_main_content {
        margin-top: 0px;
        margin-bottom: 0px
    }

    #footer .wf-wrap,
    .content-fullwidth.mini-boxed-layout #page #footer .wf-wrap .qfe_row {
        padding: 0;
    }
</style>
<!--  ************end************* -->
</footer>`;
$(function () {
  //$("#page").prepend($header);
  $("#page").append($footer);
});
